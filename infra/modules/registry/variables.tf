variable "location" {
  description = "Location for the resources"
  type        = string
}

variable "registry_name" {
  description = "Name of the Container Registry"
  type        = string
}

variable "geo_replica_location" {
  description = "Location for the geo-replicated Container Registry"
  type        = string
  default     = "eastus"
}

variable "key_vault_key_id" {
  description = "ID of the Key Vault Key"
  type        = string
}

variable "resource_group_name" {
  description = "Name of the resource group"
  type        = string
}

variable "insights_name" {
  description = "Name of the Application Insights instance"
  type        = string
}

variable "key_vault_id" {
  description = "ID of the Key Vault"
  type        = string
}

variable "vnet_id" {
  description = "ID of the virtual network"
  type        = string
}

variable "allowed_ip_range" {
  description = "IP range allowed access to Container Registry"
  type        = string
  default     = "10.0.0.0/8" # Default to internal network
}

variable "security_email" {
  description = "Email address for security alerts"
  type        = string
  default     = "portal@nationoh.co.uk"
}