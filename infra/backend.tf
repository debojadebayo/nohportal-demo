terraform {
  backend "azurerm" {
    resource_group_name  = "terraform-state-rg-latest"
    storage_account_name = "nationohtfstatelatest"
    container_name       = "tfstatelatest"
  }
}
