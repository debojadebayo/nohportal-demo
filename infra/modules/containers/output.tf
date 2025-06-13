output "container_app_urls" {
  description = "URLs for the deployed container apps"
  value = {
    server   = azurerm_container_app.api_server.latest_revision_fqdn
    keycloak = azurerm_container_app.keycloak_server.latest_revision_fqdn
    frontend = azurerm_container_app.frontend.latest_revision_fqdn
  }
}

output "debug_subnet_id" {
  description = "Debug: Subnet ID being passed to Container App Environment"
  value       = var.subnet_id
}

output "container_env_id" {
  description = "ID of the container app environment"
  value       = azurerm_container_app_environment.container_env.id
}