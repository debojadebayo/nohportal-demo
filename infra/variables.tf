variable "location" {
  description = "Location for the resources"
  type        = string
  default     = "ukwest"
}

variable "resource_group_name" {
  description = "Name of the resource group"
  type        = string
  default     = "nationohrg"
}



variable "github_actions_service_principal_object_id" {
  description = "Object ID of the GitHub Actions service principal for Key Vault access"
  type        = string
  default     = ""
}

variable "environment" {
  description = "Environment name (dev, prod, etc.)"
  type        = string
  default     = "dev"
}

# Container App Configuration
variable "container_cpu" {
  description = "CPU allocation for the container"
  type        = number
  default     = 0.5
}

variable "container_memory" {
  description = "Memory for the container"
  type        = string
  default     = "1Gi"
}

variable "aspnetcore_environment" {
  description = "ASPNETCORE_ENVIRONMENT value"
  type        = string
  default     = "Development"
}

variable "keycloak_issuer_url" {
  description = "Keycloak issuer URL"
  type        = string
}

variable "keycloak_features" {
  description = "Keycloak features to enable"
  type        = string
  default     = "organization"
}

variable "allowed_hosts" {
  description = "Allowed hosts for the API server"
  type        = string
  default     = "*"
}

variable "api_url" {
  description = "API URL"
  type        = string
  default     = ""
}

variable "keycloak_url" {
  description = "Keycloak URL"
  type        = string
  default     = ""
}

variable "server_image" {
  description = "Image for the server container"
  type        = string
  default     = "mcr.microsoft.com/dotnet/aspnet:9.0"
}

variable "frontend_image" {
  description = "Image for the frontend container"
  type        = string
  default     = "nginx:alpine"
}

variable "image_tags" {
  description = "Map of image tags for each container"
  type        = map(string)
  default = {
    keycloak = "26.1"
  }
}

# Application Gateway Configuration
variable "domain_name" {
  description = "Domain name for the application"
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
  sensitive   = true
}

variable "app_gateway_sku_tier" {
  description = "SKU tier for the Application Gateway"
  type        = string
  default     = "Standard_v2"
}

