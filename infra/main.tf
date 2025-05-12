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

resource "random_string" "random" {
  length           = 16
  special          = false
  override_special = false
}

resource "random_password" "keycloak_db_password" {
  count            = var.keycloak_db_password == "" ? 1 : 0
  length           = 24
  special          = true
  override_special = "!#$%&*()-_=+[]{}<>:?"
}

resource "random_password" "jwt_signing_key" {
  count            = var.jwt_signing_key == "" ? 1 : 0
  length           = 32
  special          = true
  override_special = "!#$%&*()-_=+[]{}<>:?"
}


module "networking" {
  source                   = "./modules/networking"
  location                 = var.location
  resource_group_name      = azurerm_resource_group.rg.name
  container_app_urls       = {}
  ssl_certificate_password = var.ssl_certificate_password
  ssl_certificate_path     = var.ssl_certificate_path
}

module "secrets" {
  source               = "./modules/secrets"
  location             = var.location
  resource_group_name  = azurerm_resource_group.rg.name
  key_vault_name       = "nationoh-kv-${random_string.random.result}"

#   Core secrets
  db_admin_username    = var.db_admin_username
  db_admin_password    = var.db_admin_password
  keycloak_admin_username = var.keycloak_admin_username
  keycloak_admin_password = var.keycloak_admin_password
  keycloak_db_username = var.keycloak_db_username
  keycloak_db_password = var.keycloak_db_password != "" ? var.keycloak_db_password : random_password.keycloak_db_password[0].result
  ssl_certificate_password = var.ssl_certificate_password
  jwt_signing_key      = var.jwt_signing_key != "" ? var.jwt_signing_key : random_password.jwt_signing_key[0].result
  patient_db_username  = var.patient_db_username
  patient_db_password  = var.patient_db_password

#   Network configuration
  vnet_id = module.networking.vnet_id
  subnet_ids = module.networking.subnets_ids
}

module "monitoring" {
  source              = "./modules/monitoring"
  location            = var.location
  resource_group_name = azurerm_resource_group.rg.name
  insights_name       = "nationoh-insights"
  key_vault_id        = module.secrets.key_vault_id
}

module "storage" {
  source               = "./modules/storage"
  location             = var.location
  resource_group_name  = azurerm_resource_group.rg.name
  subnet_ids           = module.networking.subnets_ids
  vnet_id              = module.networking.vnet_id
  storage_account_name = "nationoh${random_string.random.result}"
  key_vault_id         = module.secrets.key_vault_id
}

resource "azurerm_key_vault_secret" "storage_access_key" {
  name         = "storage-access-key"
  value        = module.storage.primary_access_key
  key_vault_id = module.secrets.key_vault_id
}

module "database" {
  source                      = "./modules/database"
  location                    = var.location
  resource_group_name         = azurerm_resource_group.rg.name
  subnet_ids                  = module.networking.subnets_ids
  container_registry          = var.container_registry
  image_tags                  = var.image_tags
  domain_name                 = var.domain_name
  postgres_dns_zone_name      = module.networking.postgres_dns_zone_name
  db_admin_username           = var.db_admin_username
  db_admin_password           = var.db_admin_password
  container_subnet_cidr_start = module.networking.container_subnet_cidr_start
  container_subnet_cidr_end   = module.networking.container_subnet_cidr_end
  vnet_id                     = module.networking.vnet_id
  container_app_urls          = {}
}



module "registry" {
  source               = "./modules/registry"
  location             = var.location
  resource_group_name  = azurerm_resource_group.rg.name
  registry_name        = "nationohregistry${random_string.random.result}"
  key_vault_id         = module.secrets.key_vault_id
  key_vault_key_id     = module.secrets.storage_key_id
  allowed_ip_range     = "YOUR_OFFICE_IP_RANGE"
  subnet_ids           = module.networking.subnets_ids
  vnet_id              = module.networking.vnet_id
  insights_name        = "nationoh-insights"
}


module "containers" {
  source                     = "./modules/containers"
  location                   = var.location
  resource_group_name        = azurerm_resource_group.rg.name
  subnet_ids                 = module.networking.subnets_ids
  container_registry         = var.container_registry
  image_tags                 = var.image_tags
  container_app_urls         = module.networking.container_app_urls
  domain_name                = var.domain_name
  log_analytics_workspace_id = module.monitoring.log_analytics_workspace_id
}
