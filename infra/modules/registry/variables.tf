variable "location" {
  description = "Location for the resources"
  type        = string
}

variable "resource_group_name" {
  description = "Name of the resource group"
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

variable "vnet_id" {
  description = "ID of the virtual network"
  type        = string
}

variable "allowed_ip_range" {
  description = "IP range allowed access to Container Registry"
  type        = string
  default     = "10.0.0.0/8" # Default to internal network
}

variable "acr_endpoint_name" {
  description = "Name of the Container Registry endpoint"
  type        = string
}

variable "acr_connection_name" {
  description = "Name of the Container Registry connection"
  type        = string
}

variable "acr_dns_zone_name" {
  description = "Name of the Container Registry DNS zone"
  type        = string
}

variable "acr_dns_link_name" {
  description = "Name of the Container Registry DNS link"
  type        = string
}

variable "acr_dns_zone_group_name" {
  description = "Name of the Container Registry DNS zone group"
  type        = string
}

variable "subnet_ids" {
  description = "ID of the private link subnet"
  type        = map(string)
}