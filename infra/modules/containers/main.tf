# log analytics workspace 

resource "azurerm_log_analytics_workspace" "log_analytics" {
  name                = var.log_analytics_workspace_name
  location            = var.location
  resource_group_name = var.resource_group_name
  sku                 = "PerGB2018"
  retention_in_days   = 30
}


# Container Environment

resource "azurerm_container_app_environment" "container_env" {
  name                       = var.container_env_name
  location                   = var.location
  resource_group_name        = var.resource_group_name
  log_analytics_workspace_id = azurerm_log_analytics_workspace.log_analytics.id
  infrastructure_subnet_id   = var.subnet_id
}


# Server API Container 

resource "azurerm_container_app" "api_server" {
  name                         = var.server_container_app_name
  container_app_environment_id = azurerm_container_app_environment.container_env.id
  resource_group_name          = var.resource_group_name
  revision_mode                = "Single"

  template {
    container {
      name   = "server"
      image  = "${var.container_registry_url}/nationoh/server:${var.image_tags["server"]}"
      cpu    = var.container_cpu
      memory = var.container_memory

      env {
        name  = "ASPNETCORE_ENVIRONMENT"
        value = var.aspnetcore_environment
      }

      liveness_probe {
        path                = "/api-health"
        port                = 5003
        transport           = var.aspnetcore_environment == "Development" ? "http" : "https"
        initial_delay       = 60
        interval_seconds    = 15
        timeout             = 5
        failure_count_threshold   = 3
      }
    }
  }

  ingress {
    external_enabled = false
    target_port      = 5003
    transport        = var.aspnetcore_environment == "Development" ? "http" : "https"

    traffic_weight {
      percentage = 100
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
    type = "UserAssigned"
    identity_ids = [var.user_assigned_identity_id]
  }

  secret {
    name                 = "keycloak-admin-username"
    identity = var.user_assigned_identity_id
    key_vault_secret_id = var.keycloak_admin_username_secret_id
  }

  secret {
    name                 = "keycloak-admin-password"
    identity = var.user_assigned_identity_id
    key_vault_secret_id = var.keycloak_admin_password_secret_id
  }

  secret {
    name                 = "keycloak-db-username"
    identity = var.user_assigned_identity_id
    key_vault_secret_id  = var.keycloak_db_username_secret_id
  }

  secret {
    name                 = "keycloak-db-password"
    identity = var.user_assigned_identity_id
    key_vault_secret_id  = var.keycloak_db_password_secret_id
  }

  template {
    container {
      name   = "keycloak"
      image  = "${var.container_registry_url}/nationoh/keycloak:${var.image_tags["keycloak"]}"
      cpu    = 0.5
      memory = "1Gi"

      

      env {
        name  = "KC_BOOTSTRAP_ADMIN_USERNAME"
        secret_name = "keycloak-admin-username"
      }
      env {
        name  = "KC_BOOTSTRAP_ADMIN_PASSWORD"
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
        name  = "KC_DB_USERNAME"
        secret_name = "keycloak-db-username"
      }
      env {
        name  = "KC_DB_PASSWORD"
        secret_name = "keycloak-db-password"
      }
      env {
        name  = "KC_FEATURES"
        value = var.keycloak_features
      }

      liveness_probe {
        path                = "/auth/realms/master"  
        port                = 8080
        transport           = var.aspnetcore_environment == "Development" ? "http" : "https"
        initial_delay       = 120  
        interval_seconds    = 30
        timeout             = 10
        failure_count_threshold   = 3
      }
    }
  }

  ingress {
    external_enabled = false
    target_port      = 8080
    transport        = "http"

    traffic_weight {
      percentage = 100
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

  template {
    container {
      name   = "frontend"
      image  = "${var.container_registry_url}/nationoh/frontend:${var.image_tags["frontend"]}"
      cpu    = 1.0
      memory = "2Gi"

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
          path                = "/health"  
          port                = 5002
          transport           = var.aspnetcore_environment == "Development" ? "http" : "https"
          initial_delay       = 60
          interval_seconds    = 15
          timeout             = 5
          failure_count_threshold   = 3
        }
    }
    
  }


  ingress {
    external_enabled = false
    target_port      = 5002
    transport        = var.aspnetcore_environment == "Development" ? "http" : "https"

    traffic_weight {
      percentage = 100
      latest_revision = true
    }
  }
}
