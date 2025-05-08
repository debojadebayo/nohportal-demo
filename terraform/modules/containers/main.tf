# Container Environment

resource "azurerm_container_app_environment" "container_env" {
    name                = var.container_env_name
    location            = var.location
    resource_group_name = var.resource_group_name

    infrastructure_subnet_id = var.subnet_ids["backend"]
}


# Server API Container 

resource "azurerm_container_app" "server" {
    name = "ComposedHealth-server"
    container_app_environment_id = azurerm_container_app_environment.container_env.id
    resource_group_name = var.resource_group_name
    revision_mode = "Single"

    template {
        container {
            name = "server"
            image = ""
            cpu = 0.25 
            memory = "0.5Gi"

            env {
                name = "ASPNETCORE_ENVIRONMENT"
                value = "Development"
            }
        }
    }

    ingress {
        external_enabled = true
        target_port = 5003
        transport = "Http"

        traffic_weight {
            percentage = 100
        }
    }
}

# Keycloak 

resource "azurerm_container_app" "keycloak" {
    name = "ComposedHealth-keycloak"
    container_app_environment_id = azurerm_container_app_environment.container_env.id
    resource_group_name = var.resource_group_name
    revision_mode = "Single"

    template {
        container {
            name = "keycloak"
            image = "keycloak/keycloak:26.1"
            cpu = 0.5 
            memory = "1Gi"

            env {
                name = "KC_BOOTSTRAP_ADMIN_USERNAME"
                value = "admin"
            }
            env {
                name = "KC_BOOTSTRAP_ADMIN_PASSWORD"
                value = "123"
            }
            env {
                name = "KC_DB"
                value = "postgres"
            }
            env {
                name = "KC_DB_URL"
                value = "jdbc:postgresql://${azurerm_postgresql_server.keycloak_db.fqdn}:5432/keycloak"
            }
            env {
                name = "KC_DB_USERNAME"
                value = "keycloak"
            }
            env {
                name = "KC_DB_PASSWORD"
                value = "123"
            }
            env {
                name = "KC_FEATURES"
                value = "organization"
            }
        }
    }

    ingress {
        external_enabled = true
        target_port = 8080
        transport = "Http"

        traffic_weight {
            percentage = 100
        }
    }
}
    


