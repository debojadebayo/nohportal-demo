output "registry_id" {
  description = "ID of the container registry"
  value       = azurerm_container_registry.acr.id
}

output "registry_login_server" {
  description = "Login server URL for the container registry"
  value       = azurerm_container_registry.acr.login_server
}

output "acr_url" {
  description = "URL of the container registry"
  value       = azurerm_container_registry.acr.login_server
}