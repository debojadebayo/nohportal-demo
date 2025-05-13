output "postgresql_server_id" {
  description = "ID of the PostgreSQL flexible server"
  value       = azurerm_postgresql_flexible_server.postgresql_main.id
}

output "postgresql_server_fqdn" {
  description = "FQDN of the PostgreSQL flexible server"
  value       = azurerm_postgresql_flexible_server.postgresql_main.fqdn
}

output "app_database_name" {
  description = "Name of the application database"
  value       = azurerm_postgresql_flexible_server_database.app_db.name
}

output "keycloak_db_name" {
  description = "Name of the Keycloak database"
  value       = azurerm_postgresql_flexible_server_database.keycloak_db.name
}

output "postgresql_connection_string" {
  description = "PostgreSQL connection string for the application"
  value       = "postgresql://${var.db_admin_username}@${azurerm_postgresql_flexible_server.postgresql_main.name}:${var.db_admin_password}@${azurerm_postgresql_flexible_server.postgresql_main.fqdn}:5432/${azurerm_postgresql_flexible_server_database.app_db.name}"
  sensitive   = true
}