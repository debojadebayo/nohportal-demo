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
  default     = null  # Let Azure generate a name if not specified
}

variable "subnet_ids" {
  description = "Map of subnet IDs"
  type        = map(string)
}

variable "vnet_id" {
  description = "ID of the virtual network"
  type        = string
}