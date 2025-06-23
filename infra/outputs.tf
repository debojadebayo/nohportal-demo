
# Core Infrastructure Outputs

# Resource Group
output "resource_group_name" {
  description = "Name of the resource group"
  value       = azurerm_resource_group.rg.name
}

output "resource_group_location" {
  description = "Location of the resource group"
  value       = azurerm_resource_group.rg.location
}

# Networking
output "vnet_id" {
  description = "ID of the virtual network"
  value       = module.networking.vnet_id
}

output "subnet_ids" {
  description = "Map of subnet IDs"
  value       = module.networking.subnets_ids
}

# Container Registry
output "container_registry_url" {
  description = "URL of the Azure Container Registry"
  value       = module.registry.acr_url
}

output "container_registry_id" {
  description = "ID of the Azure Container Registry"
  value       = module.registry.registry_id
}

# Key Vault
output "key_vault_id" {
  description = "ID of the Key Vault"
  value       = module.secrets.key_vault_id
}


# Managed Identities
output "container_apps_identity_id" {
  description = "ID of the Container Apps managed identity"
  value       = azurerm_user_assigned_identity.container_apps_identity.id
}

output "container_apps_identity_principal_id" {
  description = "Principal ID of the Container Apps managed identity"
  value       = azurerm_user_assigned_identity.container_apps_identity.principal_id
}

# output "keycloak_identity_id" {
#   description = "ID of the Keycloak managed identity"
#   value       = azurerm_user_assigned_identity.uai_keycloak.id
# }

# output "keycloak_identity_principal_id" {
#   description = "Principal ID of the Keycloak managed identity"
#   value       = azurerm_user_assigned_identity.uai_keycloak.principal_id
# }

# Database
output "postgresql_server_fqdn" {
  description = "FQDN of the PostgreSQL server"
  value       = module.database.postgresql_server_fqdn
}

output "app_database_name" {
  description = "Name of the application database"
  value       = module.database.app_database_name
}

output "keycloak_database_name" {
  description = "Name of the Keycloak database"
  value       = module.database.keycloak_db_name
}

# Storage
output "storage_account_name" {
  description = "Name of the storage account"
  value       = module.storage.storage_account_name
}

output "storage_account_primary_access_key" {
  description = "Primary access key for the storage account"
  value       = module.storage.primary_access_key
  sensitive   = true
}

# Container App outputs
output "container_app_urls" {
  description = "Internal FQDNs of Container Apps (private, accessible via Application Gateway)"
  value       = module.containers.container_app_urls
  sensitive   = true
}

# Application Gateway outputs
output "application_gateway_fqdn" {
  description = "FQDN of the Application Gateway (public entry point)"
  value       = module.application_gateway.gateway_fqdn
}

output "application_gateway_public_ip" {
  description = "Public IP address of the Application Gateway"
  value       = module.application_gateway.gateway_public_ip
}