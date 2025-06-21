output "container_app_urls" {
  description = "URLs of the container apps"
  value = {
    server   = azurerm_container_app.api_server.latest_revision_fqdn
    keycloak = azurerm_container_app.keycloak_server.latest_revision_fqdn
    frontend = azurerm_container_app.frontend.latest_revision_fqdn
  }
}

output "container_env_default_domain" {
  description = "Default domain of the container app environment"
  value       = azurerm_container_app_environment.container_env.default_domain
}

output "container_env_static_ip" {
  description = "Static IP of the container app environment"
  value       = azurerm_container_app_environment.container_env.static_ip_address
}

output "container_env_id" {
  description = "ID of the container app environment"
  value       = azurerm_container_app_environment.container_env.id
}