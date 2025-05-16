variable "location" {
  description = "Azure region where resources will be created"
  type        = string
}

variable "resource_group_name" {
  description = "Name of the resource group"
  type        = string
}

variable "subnet_id" {
  description = "ID of the subnet"
  type        = string
}

variable "app_gateway_sku_tier" {
  description = "SKU tier for the Application Gateway"
  type        = string
}

variable "frontend_fqdn" {
  description = "FQDN of the frontend"
  type        = string
  default     = ""
}

variable "api_fqdn" {
  description = "FQDN of the API"
  type        = string
  default     = ""
}

variable "auth_fqdn" {
  description = "FQDN of the authentication"
  type        = string
  default     = ""
}

variable "ssl_certificate_path" {
  description = "Path to the SSL certificate"
  type        = string
  default     = ""
}

variable "ssl_certificate_password" {
  description = "Password for the SSL certificate"
  type        = string
  default     = ""
}