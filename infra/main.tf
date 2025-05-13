terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.43.0"
    }
  }

  required_version = ">= 1.1.0"
}

provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "rg" {
  name     = var.resource_group_name
  location = var.location
}


module "networking" {
  source                   = "./modules/networking"
  location                 = var.location
  resource_group_name      = azurerm_resource_group.rg.name
}

module "secrets" {
  source                   = "./modules/secrets"
  location                 = var.location
  resource_group_name      = azurerm_resource_group.rg.name
  subnet_ids               = module.networking.subnets_ids
  key_vault_name           = "${var.resource_group_name}-kv"
  vnet_id                  = module.networking.vnet_id
}

module "monitoring" {
    source                   = "./modules/monitoring"
    location                 = var.location
    resource_group_name      = azurerm_resource_group.rg.name
    insights_name            = "${var.resource_group_name}-insights"
    key_vault_id             = module.secrets.key_vault_id
}

module "storage" {
    source                   = "./modules/storage"
    location                 = var.location
    resource_group_name      = azurerm_resource_group.rg.name
    storage_account_name     = "${var.resource_group_name}-storage"
    vnet_id                  = module.networking.vnet_id
    subnet_ids               = module.networking.subnets_ids
    key_vault_id             = module.secrets.key_vault_id
}

module "database"{
    source                   = "./modules/database"
    location                 = var.location
    resource_group_name      = azurerm_resource_group.rg.name
    subnet_ids               = module.networking.subnets_ids
    vnet_id                  = module.networking.vnet_id
    app_database_name        = "${var.resource_group_name}-app-db"
    keycloak_db_name         = "${var.resource_group_name}-keycloak-db"
    db_admin_username        = module.secrets.postgresql_admin_username
    db_admin_password        = module.secrets.postgresql_admin_password
    container_subnet_cidr_start = module.networking.container_subnet_cidr_start
    container_subnet_cidr_end   = module.networking.container_subnet_cidr_end
    postgres_dns_zone_name = module.networking.postgres_dns_zone_name
    postgresql_server_name = module.database.postgresql_server_name
    postgresql_server_fqdn = module.database.postgresql_server_fqdn
}

