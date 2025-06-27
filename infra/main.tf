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
  features {
    resource_group {
      prevent_deletion_if_contains_resources = false
    }
  }
  subscription_id = ""
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

resource "azurerm_role_assignment" "acr_pull_role" {
  scope                = module.registry.registry_id
  role_definition_name = "AcrPull"
  principal_id         = azurerm_user_assigned_identity.container_apps_identity.principal_id

  depends_on = [
    module.registry
  ]
}

resource "azurerm_user_assigned_identity" "container_apps_identity" {
  name                = "${var.resource_group_name}-uai-container-apps"
  resource_group_name = azurerm_resource_group.rg.name
  location            = var.location
}

# Get current Azure client config 
data "azurerm_client_config" "current" {}


module "networking" {
  source              = "./modules/networking"
  location            = var.location
  resource_group_name = azurerm_resource_group.rg.name
}

module "secrets" {
  source                                     = "./modules/secrets"
  location                                   = var.location
  resource_group_name                        = azurerm_resource_group.rg.name
  subnet_ids                                 = module.networking.subnets_ids
  key_vault_name                             = "${var.resource_group_name}-kv"
  vnet_id                                    = module.networking.vnet_id
  keycloak_managed_identity_object_id        = azurerm_user_assigned_identity.uai_keycloak.principal_id
  github_actions_service_principal_object_id = var.github_actions_service_principal_object_id
}

module "monitoring" {
  source              = "./modules/monitoring"
  location            = var.location
  resource_group_name = azurerm_resource_group.rg.name
  insights_name       = "${var.resource_group_name}-insights"
  key_vault_id        = module.secrets.key_vault_id
}

module "storage" {
  source               = "./modules/storage"
  location             = var.location
  resource_group_name  = azurerm_resource_group.rg.name
  storage_account_name = "${var.resource_group_name}storage"
  vnet_id              = module.networking.vnet_id
  subnet_ids           = module.networking.subnets_ids
  key_vault_id         = module.secrets.key_vault_id
}

module "database" {
  source                      = "./modules/database"
  location                    = var.location
  resource_group_name         = azurerm_resource_group.rg.name
  subnet_ids                  = module.networking.subnets_ids
  vnet_id                     = module.networking.vnet_id
  app_database_name           = "${var.resource_group_name}appdb"
  keycloak_db_name            = "${var.resource_group_name}keycloakdb"
  db_admin_username           = module.secrets.postgresql_admin_username
  db_admin_password           = module.secrets.postgresql_admin_password
  container_subnet_cidr_start = module.networking.container_subnet_cidr_start
  container_subnet_cidr_end   = module.networking.container_subnet_cidr_end
  postgresql_server_name      = "${var.resource_group_name}postgresql"
  private_dns_zone_name       = var.resource_group_name
  dns_link_name               = "${var.resource_group_name}postgresdnslink"
  firewall_rule_name          = "${var.resource_group_name}postgresfirewallrule"
}

module "registry" {
  source                  = "./modules/registry"
  location                = var.location
  resource_group_name     = azurerm_resource_group.rg.name
  registry_name           = "${var.resource_group_name}registry"
  geo_replica_location    = "northeurope"
  vnet_id                 = module.networking.vnet_id
  allowed_ip_range        = "10.0.2.0/24"
  acr_endpoint_name       = "${var.resource_group_name}acr-endpoint"
  acr_connection_name     = "${var.resource_group_name}acr-connection"
  acr_dns_zone_name       = var.resource_group_name
  acr_dns_link_name       = "${var.resource_group_name}acr-dnslink"
  acr_dns_zone_group_name = "${var.resource_group_name}acr-dnszonegroup"
  subnet_ids              = module.networking.subnets_ids
}

module "containers" {
  source                     = "./modules/containers"
  location                   = var.location
  resource_group_name        = azurerm_resource_group.rg.name
  subnet_id                  = module.networking.subnets_ids["backend"]
  log_analytics_workspace_id = module.monitoring.log_analytics_workspace_id
  container_registry_url     = module.registry.acr_url
  container_cpu              = var.container_cpu
  container_memory           = var.container_memory
  container_env_name         = "${var.resource_group_name}env"

  # Managed identity 
  container_apps_identity_id = azurerm_user_assigned_identity.container_apps_identity.id
  keycloak_identity_id       = azurerm_user_assigned_identity.uai_keycloak.id

  # API Server 
  server_container_app_name = "${var.resource_group_name}server"

  # Server environment variables
  keycloak_issuer_url                  = var.keycloak_issuer_url
  allowed_hosts                        = var.allowed_hosts
  app_database_connection_string       = "Host=${module.database.postgresql_server_fqdn};Port=5432;Database=${module.database.app_database_name};Username=${module.secrets.postgresql_admin_username};Password=${module.secrets.postgresql_admin_password};SSL Mode=Require;Trust Server Certificate=true"
  azure_blob_storage_connection_string = "DefaultEndpointsProtocol=https;AccountName=${module.storage.storage_account_name};AccountKey=${module.storage.primary_access_key};EndpointSuffix=core.windows.net"

  # Keycloak
  keycloak_container_app_name       = "${var.resource_group_name}keycloak"
  keycloak_features                 = var.keycloak_features
  keycloak_db_url                   = "jdbc:postgresql://${module.database.postgresql_server_fqdn}:5432/${module.database.keycloak_db_name}"
  keycloak_admin_username_secret_id = module.secrets.keycloak_admin_username_secret_id
  keycloak_admin_password_secret_id = module.secrets.keycloak_admin_password_secret_id
  keycloak_db_username_secret_id    = module.secrets.keycloak_db_username_secret_id
  keycloak_db_password_secret_id    = module.secrets.keycloak_db_password_secret_id

  # Frontend 
  frontend_container_app_name = "${var.resource_group_name}frontend"
  api_url                     = "https://${var.resource_group_name}-gateway.${var.location}.cloudapp.azure.com/api"
  keycloak_url                = "https://${var.resource_group_name}-gateway.${var.location}.cloudapp.azure.com/auth"

  # Container Images (applying commit 244a9e0 changes)
  server_image   = var.server_image
  frontend_image = var.frontend_image

  # Environment
  aspnetcore_environment = var.aspnetcore_environment
  image_tags             = var.image_tags

  depends_on = [
    module.database,
    module.secrets,
    module.registry,
    azurerm_role_assignment.acr_pull_role
  ]
}

module "application_gateway" {
  source                   = "./modules/application_gateway"
  location                 = var.location
  resource_group_name      = azurerm_resource_group.rg.name
  subnet_id                = module.networking.subnets_ids["appgateway"]
  ssl_certificate_path     = var.ssl_certificate_path
  ssl_certificate_password = var.ssl_certificate_password
  app_gateway_sku_tier     = var.app_gateway_sku_tier

  # Backend FQDNs from container apps
  frontend_fqdn = module.containers.container_app_urls.frontend
  api_fqdn      = module.containers.container_app_urls.server
  auth_fqdn     = module.containers.container_app_urls.keycloak

  depends_on = [module.containers]
}

# Private DNS Zone for Container Apps (created after containers are deployed)
resource "azurerm_private_dns_zone" "container_apps" {
  name                = module.containers.container_env_default_domain
  resource_group_name = azurerm_resource_group.rg.name

  depends_on = [module.containers]
}

# Link the Container Apps DNS Zone to the VNet
resource "azurerm_private_dns_zone_virtual_network_link" "container_apps" {
  name                  = "container-apps-dns-link"
  resource_group_name   = azurerm_resource_group.rg.name
  private_dns_zone_name = azurerm_private_dns_zone.container_apps.name
  virtual_network_id    = module.networking.vnet_id

  depends_on = [module.containers, module.networking]
}

# Wildcard A-record for Container Apps
resource "azurerm_private_dns_a_record" "container_apps_wildcard" {
  name                = "*"
  zone_name           = azurerm_private_dns_zone.container_apps.name
  resource_group_name = azurerm_resource_group.rg.name
  ttl                 = 300
  records             = [module.containers.container_env_static_ip]

  depends_on = [azurerm_private_dns_zone.container_apps]
}

# Root A-record for Container Apps
resource "azurerm_private_dns_a_record" "container_apps_root" {
  name                = "@"
  zone_name           = azurerm_private_dns_zone.container_apps.name
  resource_group_name = azurerm_resource_group.rg.name
  ttl                 = 300
  records             = [module.containers.container_env_static_ip]

  depends_on = [azurerm_private_dns_zone.container_apps]
}

# Test VM for connectivity testing (dev only)
module "test_vm" {
  source              = "./modules/test_vm"
  location            = var.location
  resource_group_name = azurerm_resource_group.rg.name
  subnet_id           = module.networking.subnets_ids["vm"]

  depends_on = [module.networking]
}