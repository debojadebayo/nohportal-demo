provider "azurerm" {
  features {}
}

terraform {
  backend "azurerm" {
    resource_group_name  = "nohportaldemo-rg"
    storage_account_name = "nohportaldemosa"
    container_name       = "tfstate"
    key                  = "terraform-base.tfstate"
  }
}

data "azurerm_client_config" "current" {}