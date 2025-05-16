variable "location" {
  description = "Location for the resources"
  type        = string
}

variable "resource_group_name" {
  description = "Name of the resource group"
  type        = string
}


variable "storage_account_name" {
  description = "Name of the storage account"
  type        = string
  default = null
}

variable "subnet_ids" {
  description = "Map of subnet IDs"
  type        = map(string)
}

variable "vnet_id" {
  description = "ID of the virtual network"
  type        = string
}

variable "key_vault_id" {
  description = "ID of the key vault"
  type        = string
}