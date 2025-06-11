# Application Gateway outputs
output "application_gateway_fqdn" {
  description = "FQDN of the Application Gateway (public entry point)"
  value       = module.application_gateway.gateway_fqdn
}

output "application_gateway_public_ip" {
  description = "Public IP address of the Application Gateway"
  value       = module.application_gateway.gateway_public_ip
}

# Container App outputs
output "container_app_urls" {
  description = "Internal FQDNs of Container Apps (private, accessible via Application Gateway)"
  value       = module.containers.container_app_urls
  sensitive   = true
}

# Resource group output
output "resource_group_name" {
  description = "Name of the resource group"
  value       = azurerm_resource_group.rg.name
}