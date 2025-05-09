terraform {
    required_providers {
        azurerm = {
            source = "hashicorp/azurerm"
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
    source = "./modules/networking"
    location = var.location
    resource_group_name = azurerm_resource_group.rg.name
    container_app_urls = module.containers.container_app_urls
    ssl_certificate_password = var.ssl_certificate_password
    ssl_certificate_path = var.ssl_certificate_path
}

module "containers" {
    source = "./modules/containers"
    location = var.location
    resource_group_name = azurerm_resource_group.rg.name
    subnet_ids = module.networking.subnets_ids
    container_registry = var.container_registry
    image_tags = var.image_tags
    domain_name = var.domain_name
    container_app_urls = module.networking.container_app_urls
}

module "database" {
    source = "./modules/database"
    location = var.location
    resource_group_name = azurerm_resource_group.rg.name
    subnet_ids = module.networking.subnets_ids
    container_registry = var.container_registry
    image_tags = var.image_tags
    domain_name = var.domain_name
    postgres_dns_zone_name = module.networking.postgres_dns_zone_name
    db_admin_username = var.db_admin_username
    db_admin_password = var.db_admin_password
    container_subnet_cidr_start = module.networking.container_subnet_cidr_start
    container_subnet_cidr_end = module.networking.container_subnet_cidr_end
    vnet_id = module.networking.vnet_id
    container_app_urls = module.containers.container_app_urls
}


module "storage" {
  source              = "./modules/storage"
  location            = var.location
  resource_group_name = azurerm_resource_group.rg.name
  subnet_ids          = module.networking.subnets_ids
  vnet_id             = module.networking.vnet_id
  storage_account_name = "composedhealth${random_string.suffix.result}"
}
