variable "key_vault_name" {
  description = "Key vault name"
  type        = string
}

variable "location" {
  description = "Location for the resources"
  type        = string
}

variable "resource_group_name" {
  description = "Name of the resource group"
  type        = string
}

variable "subnet_ids" {
  description = "Map of subnet names to subnet IDs"
  type        = map(string)
}

variable "vnet_id" {
  description = "Virtual network ID"
  type        = string
}