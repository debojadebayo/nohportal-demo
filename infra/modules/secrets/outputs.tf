output "key_vault_uri" {
  description = "The URI of the Key Vault"
  value       = azurerm_key_vault.kv.vault_uri
}

output "key_vault_id" {
  description = "The ID of the Key Vault"
  value       = azurerm_key_vault.kv.id
}

# output "key_vault_name" {
#   description = "The name of the Key Vault"
#   value       = azurerm_key_vault.kv.name
# }

output "postgresql_admin_username" {
    description = "PostgreSQL admin username"
    value       = azurerm_key_vault_secret.postgresql_admin_username.value
}

output "postgresql_admin_password" {
    description = "PostgreSQL admin password"
    value       = azurerm_key_vault_secret.postgresql_admin_password.value
}
