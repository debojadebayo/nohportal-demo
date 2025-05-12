output "container_app_urls" {
  description = "URLs for the deployed container apps"
  value = {
    server   = azurerm_container_app.server.latest_revision_fqdn
    keycloak = azurerm_container_app.keycloak.latest_revision_fqdn
    frontend = azurerm_container_app.frontend.latest_revision_fqdn
  }
}

output "container_env_id" {
  description = "ID of the container app environment"
  value       = azurerm_container_app_environment.container_env.id
}