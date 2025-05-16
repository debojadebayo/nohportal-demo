terraform {
    backend "azurerm" {
        resource_group_name = "terraform-state-rg"
        storage_account_name = "nationoh-tfstate"
        container_name = "tfstate"
    }
}