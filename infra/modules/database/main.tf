
# PostgreSQL Flexible Server
resource "azurerm_postgresql_flexible_server" "postgresql_main" {
  name                   = var.postgresql_server_name
  resource_group_name    = var.resource_group_name
  location               = var.location
  version                = "14"
  delegated_subnet_id    = var.subnet_ids["data"]
  private_dns_zone_id    = azurerm_private_dns_zone.postgres.id
  administrator_login    = var.db_admin_username
  administrator_password = var.db_admin_password
  zone                   = "1"
  storage_mb             = 32768
  sku_name               = "GP_Standard_D2s_v3"
  backup_retention_days  = 7

  high_availability {
    mode = "ZoneRedundant"
  }

  depends_on = [azurerm_private_dns_zone_virtual_network_link.postgres]
}

# Private DNS Zone for PostgreSQL
resource "azurerm_private_dns_zone" "postgres" {
  name                = "${var.private_dns_zone_name}.postgres.database.azure.com"
  resource_group_name = var.resource_group_name
}

# Link the DNS Zone to the VNet
resource "azurerm_private_dns_zone_virtual_network_link" "postgres" {
  name                  = var.dns_link_name
  resource_group_name   = var.resource_group_name
  private_dns_zone_name = azurerm_private_dns_zone.postgres.name
  virtual_network_id    = var.vnet_id
}

# Application Database
resource "azurerm_postgresql_flexible_server_database" "app_db" {
  name      = var.app_database_name
  server_id = azurerm_postgresql_flexible_server.postgresql_main.id
  charset   = "UTF8"
  collation = "en_US.utf8"

  # To be deployed in production
  # lifecycle {
  #   prevent_destroy = true
  # }
}

# Keycloak Database
resource "azurerm_postgresql_flexible_server_database" "keycloak_db" {
  name      = var.keycloak_db_name
  server_id = azurerm_postgresql_flexible_server.postgresql_main.id
  charset   = "UTF8"
  collation = "en_US.utf8"


  # To be deployed in production
  #   lifecycle {
  #     prevent_destroy = true
  #   }
}

# Firewall rule to allow connections from the container apps subnet
resource "azurerm_postgresql_flexible_server_firewall_rule" "container_apps" {
  name             = var.firewall_rule_name
  server_id        = azurerm_postgresql_flexible_server.postgresql_main.id
  start_ip_address = var.container_subnet_cidr_start
  end_ip_address   = var.container_subnet_cidr_end
}
    