output "key_vault_uri" {
  description = "The URI of the Key Vault"
  value       = azurerm_key_vault.kv.vault_uri
}

output "key_vault_id" {
  description = "The ID of the Key Vault"
  value       = azurerm_key_vault.kv.id
}

output "key_vault_name" {
  description = "The name of the Key Vault"
  value       = azurerm_key_vault.kv.name
}

output "storage_key_id" {
  description = "ID of the storage encryption key"
  value       = azurerm_key_vault_key.storage_key.id
}