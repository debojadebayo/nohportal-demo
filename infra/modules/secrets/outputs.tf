output "key_vault_uri" {
  description = "The URI of the Key Vault"
  value       = azurerm_key_vault.kv.vault_uri
}

output "key_vault_id" {
  description = "The ID of the Key Vault"
  value       = azurerm_key_vault.kv.id
}

output "postgresql_admin_username" {
    description = "PostgreSQL admin username"
    value       = azurerm_key_vault_secret.postgresql_admin_username.value
}

output "postgresql_admin_password" {
    description = "PostgreSQL admin password"
    value       = azurerm_key_vault_secret.postgresql_admin_password.value
}

output "keycloak_admin_username_secret_id" {
    description = "Keycloak admin username secret ID"
    value       = azurerm_key_vault_secret.keycloak_admin_username.id
}

output "keycloak_admin_password_secret_id" {
    description = "Keycloak admin password secret ID"
    value       = azurerm_key_vault_secret.keycloak_admin_password.id
}

output "keycloak_db_username_secret_id" {
    description = "Keycloak DB username secret ID"
    value       = azurerm_key_vault_secret.keycloak_db_username.id
}

output "keycloak_db_password_secret_id" {
    description = "Keycloak DB password secret ID"
    value       = azurerm_key_vault_secret.keycloak_db_password.id
}