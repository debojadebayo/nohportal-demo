# Container Environment
resource "azurerm_container_app_environment" "container_env" {
  name                           = var.container_env_name
  location                       = var.location
  resource_group_name            = var.resource_group_name
  log_analytics_workspace_id     = var.log_analytics_workspace_id
  infrastructure_subnet_id       = var.subnet_id
  internal_load_balancer_enabled = true

  workload_profile {
    name                  = "Consumption"
    workload_profile_type = "Consumption"
  }
}

# Server API Container 
resource "azurerm_container_app" "api_server" {
  name                         = var.server_container_app_name
  container_app_environment_id = azurerm_container_app_environment.container_env.id
  resource_group_name          = var.resource_group_name
  revision_mode                = "Single"

  identity {
    type         = "UserAssigned"
    identity_ids = [var.container_apps_identity_id]
  }

  registry {
    server   = var.container_registry_url
    identity = var.container_apps_identity_id
  }


  template {
    min_replicas = 1
    max_replicas = 3

    container {
      name   = "server"
      image  = var.server_image
      cpu    = var.container_cpu
      memory = var.container_memory

      env {
        name  = "ASPNETCORE_ENVIRONMENT"
        value = var.aspnetcore_environment
      }
      env {
        name  = "ASPNETCORE_URLS"
        value = "http://+:8080"
      }
      env {
        name  = "ConnectionStrings__DefaultConnection"
        value = var.app_database_connection_string
      }
      env {
        name  = "ConnectionStrings__AzureBlobStorage"
        value = var.azure_blob_storage_connection_string
      }

      env {
        name  = "IdentityConfig__Issuer"
        value = var.keycloak_issuer_url
      }

      # Keycloak authentication
      env {
        name  = "IdentityConfig__Audience"
        value = "nationoh_webapi"
      }
      env {
        name  = "IdentityConfig__RequireHttpsMetadata"
        value = "true"
      }
      env {
        name  = "AllowedHosts"
        value = var.allowed_hosts
      }

      # Module configuration (from appsettings.json)
      env {
        name  = "Modules__0"
        value = "Billing"
      }
      env {
        name  = "Modules__1"
        value = "CRM"
      }
      env {
        name  = "Modules__2"
        value = "Clinical"
      }
      env {
        name  = "Modules__3"
        value = "Schedule"
      }

      liveness_probe {
        path                    = "/health"
        port                    = 8080
        transport               = "HTTP"
        initial_delay           = 30
        interval_seconds        = 30
        timeout                 = 10
        failure_count_threshold = 3
      }
    }
  }

  ingress {
    external_enabled = false
    target_port      = 8080
    transport        = "http"

    traffic_weight {
      percentage      = 100
      latest_revision = true
    }
  }
}

# Keycloak 
resource "azurerm_container_app" "keycloak_server" {
  name                         = var.keycloak_container_app_name
  container_app_environment_id = azurerm_container_app_environment.container_env.id
  resource_group_name          = var.resource_group_name
  revision_mode                = "Single"

  identity {
    type         = "UserAssigned"
    identity_ids = [var.keycloak_identity_id, var.container_apps_identity_id]
  }

  secret {
    name                = "keycloak-admin-username"
    identity            = var.keycloak_identity_id
    key_vault_secret_id = var.keycloak_admin_username_secret_id
  }

  secret {
    name                = "keycloak-admin-password"
    identity            = var.keycloak_identity_id
    key_vault_secret_id = var.keycloak_admin_password_secret_id
  }

  secret {
    name                = "keycloak-db-username"
    identity            = var.keycloak_identity_id
    key_vault_secret_id = var.keycloak_db_username_secret_id
  }

  secret {
    name                = "keycloak-db-password"
    identity            = var.keycloak_identity_id
    key_vault_secret_id = var.keycloak_db_password_secret_id
  }

  template {
    min_replicas = 1
    max_replicas = 3

    container {
      name   = "keycloak"
      image  = "quay.io/keycloak/keycloak:${var.image_tags.keycloak}"
      cpu    = 0.5
      memory = "1Gi"
      args   = ["start", "--optimized"]

      env {
        name        = "KC_BOOTSTRAP_ADMIN_USERNAME"
        secret_name = "keycloak-admin-username"
      }
      env {
        name        = "KC_BOOTSTRAP_ADMIN_PASSWORD"
        secret_name = "keycloak-admin-password"
      }
      env {
        name  = "KC_DB"
        value = "postgres"
      }
      env {
        name  = "KC_DB_URL"
        value = var.keycloak_db_url
      }
      env {
        name        = "KC_DB_USERNAME"
        secret_name = "keycloak-db-username"
      }
      env {
        name        = "KC_DB_PASSWORD"
        secret_name = "keycloak-db-password"
      }
      env {
        name  = "KC_FEATURES"
        value = var.keycloak_features
      }

      env {
        name  = "KC_HTTP_ENABLED"
        value = "true"
      }
      env {
        name  = "KC_HTTP_PORT"
        value = "8080"
      }
      env {
        name  = "KC_PROXY"
        value = "edge"
      }
      env {
        name  = "KC_HOSTNAME_STRICT"
        value = "false"
      }

      liveness_probe {
        path                    = "/health/live"
        port                    = 8080
        transport               = "HTTP"
        initial_delay           = 30
        interval_seconds        = 30
        timeout                 = 10
        failure_count_threshold = 5
      }
    }
  }

  ingress {
    external_enabled = false
    target_port      = 8080
    transport        = "http"

    traffic_weight {
      percentage      = 100
      latest_revision = true
    }
  }
}

# Frontend 
resource "azurerm_container_app" "frontend" {
  name                         = var.frontend_container_app_name
  container_app_environment_id = azurerm_container_app_environment.container_env.id
  resource_group_name          = var.resource_group_name
  revision_mode                = "Single"

  identity {
    type         = "UserAssigned"
    identity_ids = [var.container_apps_identity_id]
  }

  registry {
    server   = var.container_registry_url
    identity = var.container_apps_identity_id
  }

  template {
    min_replicas = 1
    max_replicas = 3

    container {
      name   = "frontend"
      image  = var.frontend_image
      cpu    = var.container_cpu
      memory = var.container_memory

      env {
        name  = "ASPNETCORE_ENVIRONMENT"
        value = var.aspnetcore_environment
      }
      env {
        name  = "API_URL"
        value = var.api_url
      }
      env {
        name  = "KEYCLOAK_URL"
        value = var.keycloak_url
      }

      liveness_probe {
        path                    = "/health"
        port                    = 8080
        transport               = "HTTP"
        initial_delay           = 30
        interval_seconds        = 30
        timeout                 = 10
        failure_count_threshold = 3
      }
    }
  }

  ingress {
    external_enabled = false
    target_port      = 8080
    transport        = "http"

    traffic_weight {
      percentage      = 100
      latest_revision = true
    }
  }
}