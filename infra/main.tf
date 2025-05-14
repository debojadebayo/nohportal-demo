terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 4.0.0"
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

resource "azurerm_user_assigned_identity" "uai_keycloak" {
  name                = "${var.resource_group_name}-uai-keycloak"
  location            = var.location
  resource_group_name = azurerm_resource_group.rg.name
}

resource "azurerm_role_assignment" "ra_keycloak" {
  scope = module.secrets.key_vault_id
  role_definition_name = "Key Vault Secrets Officer"
  principal_id = azurerm_user_assigned_identity.uai_keycloak.principal_id
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
    postgresql_server_name   = "${var.resource_group_name}-postgresql"
    private_dns_zone_name = "${var.resource_group_name}-postgres-dns-zone-name"
    dns_link_name = "${var.resource_group_name}-postgres-dns-link"
    firewall_rule_name = "${var.resource_group_name}-postgres-firewall-rule"
}

module "registry" {
    source                   = "./modules/registry"
    location                 = var.location
    resource_group_name      = azurerm_resource_group.rg.name
    registry_name            = "${var.resource_group_name}-registry"
    geo_replica_location     = "northeurope"
    vnet_id                  = module.networking.vnet_id
    allowed_ip_range         = "10.0.2.0/24"
    acr_endpoint_name        = "${var.resource_group_name}-acr-endpoint"
    acr_connection_name      = "${var.resource_group_name}-acr-connection"
    acr_dns_zone_name        = "${var.resource_group_name}-acr-dns-zone"
    acr_dns_link_name        = "${var.resource_group_name}-acr-dns-link"
    acr_dns_zone_group_name  = "${var.resource_group_name}-acr-dns-zone-group"
    subnet_id               = module.networking.subnets_ids["backend"]
}

module "containers" {
  source                   = "./modules/containers"
  location                 = var.location
  resource_group_name      = azurerm_resource_group.rg.name
  subnet_id                = module.networking.subnets_ids["backend"]
  container_registry_url   = module.registry.acr_url
  server_container_app_name = "${var.resource_group_name}-server"
  log_analytics_workspace_name = "${var.resource_group_name}-insights"

  # Keycloak
  keycloak_container_app_name = "${var.resource_group_name}-keycloak"
  keycloak_features = "organization"
  keycloak_db_url = "jdbc:postgresql://${module.database.postgresql_server_fqdn}:5432/${module.database.keycloak_db_name}"  
  user_assigned_identity_id = azurerm_user_assigned_identity.uai_keycloak.id
  keycloak_admin_username_secret_id = module.secrets.keycloak_admin_username_secret_id
  keycloak_admin_password_secret_id = module.secrets.keycloak_admin_password_secret_id
  keycloak_db_username_secret_id = module.secrets.keycloak_db_username_secret_id
  keycloak_db_password_secret_id = module.secrets.keycloak_db_password_secret_id
}