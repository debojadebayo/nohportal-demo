data "azurerm_resource_group" "rg" {
  name = "${var.app_name}-rg"
}

data "azurerm_log_analytics_workspace" "la" {
  name                = "${var.app_name}la"
  resource_group_name = data.azurerm_resource_group.rg.name
}

data "azurerm_container_registry" "acr" {
  name                = "${var.app_name}acracr"
  resource_group_name = data.azurerm_resource_group.rg.name
}

resource "azurerm_postgresql_flexible_server" "postgres" {
  name                   = "${var.app_name}-postgres"
  resource_group_name    = data.azurerm_resource_group.rg.name
  location               = data.azurerm_resource_group.rg.location
  version                = "13"
  administrator_login    = var.postgres_user
  administrator_password = var.postgres_password
  storage_mb             = 32768
  sku_name               = "B_Standard_B1ms" # Low-cost dev/test tier
  zone                   = "1"

  #   high_availability {
  #     mode = "ZoneRedundant"
  #     standby_availability_zone = "1"
  #   }
}


resource "azurerm_postgresql_flexible_server_firewall_rule" "azure_services" {
  name             = "AllowAzureServices"
  server_id        = azurerm_postgresql_flexible_server.postgres.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "0.0.0.0"
}

resource "azapi_resource" "containerapp_environment" {
  type      = "Microsoft.App/managedEnvironments@2022-03-01"
  name      = "${var.app_name}acaenv"
  parent_id = data.azurerm_resource_group.rg.id
  location  = data.azurerm_resource_group.rg.location

  body = {
    properties = {
      appLogsConfiguration = {
        destination = "log-analytics"
        logAnalyticsConfiguration = {
          customerId = data.azurerm_log_analytics_workspace.la.workspace_id
          sharedKey  = data.azurerm_log_analytics_workspace.la.primary_shared_key
        }
      }
    }
  }
}

resource "azurerm_user_assigned_identity" "containerapp" {
  location            = data.azurerm_resource_group.rg.location
  name                = "containerappmi"
  resource_group_name = data.azurerm_resource_group.rg.name
}

resource "azurerm_role_assignment" "containerapp" {
  scope                = data.azurerm_resource_group.rg.id
  role_definition_name = "acrpull"
  principal_id         = azurerm_user_assigned_identity.containerapp.principal_id
  depends_on = [
    azurerm_user_assigned_identity.containerapp
  ]
}

# Server Container App
resource "azapi_resource" "containerapp_server" {
  type      = "Microsoft.App/containerapps@2022-03-01"
  name      = "${var.app_name}server"
  parent_id = data.azurerm_resource_group.rg.id
  location  = data.azurerm_resource_group.rg.location

  identity {
    type         = "SystemAssigned, UserAssigned"
    identity_ids = [azurerm_user_assigned_identity.containerapp.id]
  }
  body = {
    properties = {
      managedEnvironmentId = azapi_resource.containerapp_environment.id
      configuration = {
        ingress = {
          external : true,
          targetPort : 8080
        },
        "registries" : [
          {
            "server" : data.azurerm_container_registry.acr.login_server,
            "identity" : azurerm_user_assigned_identity.containerapp.id
          }
        ]
      }
      template = {
        containers = [
          {
            image = "${data.azurerm_container_registry.acr.login_server}/app.server:${var.GITHUB_SHA}",
            name  = "appserver"
            resources = {
              cpu    = 0.25
              memory = "0.5Gi"
            },
            env = [
              {
                name  = "ConnectionStrings__DefaultConnection"
                value = "Host=${azurerm_postgresql_flexible_server.postgres.fqdn};Port=5432;User ID=${var.postgres_user};Password=${var.postgres_password};Database=${var.postgres_db};"
              },
              {
                name  = "ASPNETCORE_ENVIRONMENT"
                value = "Development"
              },
              {
                name  = "ASPNETCORE_URLS"
                value = "http://0.0.0.0:8080"
              }
            ],
            "probes" : [
              {
                "type" : "Liveness",
                "httpGet" : {
                  "path" : "/api-health",
                  "port" : 8080,
                  "scheme" : "HTTP"
                },
                "periodSeconds" : 10
              },
              {
                "type" : "Readiness",
                "httpGet" : {
                  "path" : "/api-health",
                  "port" : 8080,
                  "scheme" : "HTTP"
                },
                "periodSeconds" : 10
              },
              {
                "type" : "Startup",
                "httpGet" : {
                  "path" : "/api-health",
                  "port" : 8080,
                  "scheme" : "HTTP"
                },
                "periodSeconds" : 10
              }
            ]
          }
        ]
        scale = {
          minReplicas = 1,
          maxReplicas = 2
        }
      }
    }
  }
  ignore_missing_property = true
  depends_on = [
    azapi_resource.containerapp_environment,
    azurerm_postgresql_flexible_server.postgres,
    azurerm_postgresql_flexible_server_firewall_rule.azure_services
  ]
}

# Client Container App
resource "azapi_resource" "containerapp_client" {
  type      = "Microsoft.App/containerapps@2022-03-01"
  name      = "${var.app_name}client"
  parent_id = data.azurerm_resource_group.rg.id
  location  = data.azurerm_resource_group.rg.location

  identity {
    type         = "SystemAssigned, UserAssigned"
    identity_ids = [azurerm_user_assigned_identity.containerapp.id]
  }
  body = {
    properties = {
      managedEnvironmentId = azapi_resource.containerapp_environment.id
      configuration = {
        ingress = {
          external : true,
          targetPort : 8080
        },
        "registries" : [
          {
            "server" : data.azurerm_container_registry.acr.login_server,
            "identity" : azurerm_user_assigned_identity.containerapp.id
          }
        ]
      }
      template = {
        containers = [
          {
            image = "${data.azurerm_container_registry.acr.login_server}/app.client:${var.GITHUB_SHA}",
            name  = "appclient"
            resources = {
              cpu    = 0.25
              memory = "0.5Gi"
            },
            env = [
              {
                name  = "ASPNETCORE_ENVIRONMENT"
                value = "Development"
              },
            ],
            "probes" : [
              {
                "type" : "Liveness",
                "httpGet" : {
                  "path" : "/health",
                  "port" : 8080,
                  "scheme" : "HTTP"
                },
                "periodSeconds" : 10
              },
              {
                "type" : "Readiness",
                "httpGet" : {
                  "path" : "/health",
                  "port" : 8080,
                  "scheme" : "HTTP"
                },
                "periodSeconds" : 10
              },
              {
                "type" : "Startup",
                "httpGet" : {
                  "path" : "/health",
                  "port" : 8080,
                  "scheme" : "HTTP"
                },
                "periodSeconds" : 10
              }
            ]
          }
        ]
        scale = {
          minReplicas = 1,
          maxReplicas = 2
        }
      }
    }
  }
  ignore_missing_property = true
  depends_on = [
    azapi_resource.containerapp_environment
  ]
}