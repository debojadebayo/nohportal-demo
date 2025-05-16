terraform {
  backend "azurerm" {
    resource_group_name  = "terraform-state-rg-state"
    storage_account_name = "nationohtfstatefilestest"
    container_name       = "tfstate"
  }
}
