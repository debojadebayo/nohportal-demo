# output "registry_id" {
#   description = "ID of the container registry"
#   value       = azurerm_container_registry.acr.id
# }

output "registry_login_server" {
  description = "Login server URL for the container registry"
  value       = azurerm_container_registry.acr.login_server
}

# output "registry_admin_username" {
#   description = "Admin username for the container registry"
#   value       = azurerm_container_registry.acr.admin_username
#   sensitive   = true
# }

# output "registry_admin_password" {
#   description = "Admin password for the container registry"
#   value       = azurerm_container_registry.acr.admin_password
#   sensitive   = true
# }