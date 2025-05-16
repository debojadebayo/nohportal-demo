variable "location" {
  description = "Location for the resources"
  type        = string
  default     = "ukwest"
}

variable "resource_group_name" {
  description = "Name of the resource group"
  type        = string
  default     = "nationoh-rg"
}

variable "container_cpu" {
  description = "CPU Allocation for the container"
  type        = number
  default     = 1.0
}

variable "container_memory" {
  description = "Memory for the container"
  type        = string
  default     = "2Gi"
}

variable "aspnetcore_environment" {
  description = "ASPNETCORE_ENVIRONMENT value"
  type        = string
  default     = "Development"
}

variable "domain_name" {
  description = "Domain name"
  type        = string
  default     = ""
}

variable "ssl_certificate_password" {
  description = "Password for the SSL certificate"
  type        = string
  default     = ""
}

variable "ssl_certificate_path" {
  description = "Path to the SSL certificate"
  type        = string
  default     = ""
}

variable "app_gateway_sku_tier" {
  description = "SKU tier for the Application Gateway"
  type        = string
  default     = "WAF_v2"
}
