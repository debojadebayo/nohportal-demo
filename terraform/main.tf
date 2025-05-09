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
}

module "containers" {
    source = "./modules/containers"
    location = var.location
    resource_group_name = azurerm_resource_group.rg.name
    subnet_ids = module.networking.subnets_ids
    container_registry = var.container_registry
    image_tags = var.image_tags
    domain_name = var.domain_name
}
