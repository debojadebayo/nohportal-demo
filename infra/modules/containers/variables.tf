variable "location" {
  description = "Azure region where resources will be created"
  type        = string
}

variable "resource_group_name" {
  description = "Name of the resource group"
  type        = string
}

variable "container_env_name" {
  description = "Name of the Container App Environment"
  type        = string
}

variable "subnet_id" {
  description = "ID of the subnet for container apps"
  type        = string
}

variable "log_analytics_workspace_id" {
  description = "ID of the Log Analytics workspace"
  type        = string
}

variable "aspnetcore_environment" {
  description = "ASPNETCORE_ENVIRONMENT value"
  type        = string
  default     = "Development"
}

variable "container_cpu" {
  description = "CPU for the container"
  type        = number
}

variable "container_memory" {
  description = "Memory for the container"
  type        = string
}

variable "container_registry_url" {
  description = "Container registry URL"
  type        = string
}

variable "container_apps_identity_id" {
  description = "Container Apps Identity ID"
  type        = string
}

variable "keycloak_identity_id" {
  description = "Keycloak Identity ID"
  type        = string
}

variable "server_container_app_name" {
  description = "Name of the server container app"
  type        = string
}

variable "keycloak_container_app_name" {
  description = "Name of the keycloak container app"
  type        = string
}

variable "frontend_container_app_name" {
  description = "Name of the frontend container app"
  type        = string
}

variable "api_url" {
  description = "API URL"
  type        = string
}

variable "keycloak_url" {
  description = "Keycloak URL"
  type        = string
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

variable "keycloak_db_url" {
  description = "Keycloak DB JDBC URL"
  type        = string
}

variable "image_tags" {
  description = "Map of image tags for each container"
  type        = map(string)
  default = {
    keycloak = "26.1"
  }
}

variable "app_database_connection_string_secret_id" {
  description = "Key Vault secret ID for app database connection string"
  type        = string
}

variable "azure_blob_storage_connection_string_secret_id" {
  description = "Key Vault secret ID for Azure blob storage connection string"
  type        = string
}

variable "keycloak_admin_username_secret_id" {
  description = "Keycloak admin username secret ID"
  type        = string
}

variable "keycloak_admin_password_secret_id" {
  description = "Keycloak admin password secret ID"
  type        = string
}

variable "keycloak_db_username_secret_id" {
  description = "Keycloak DB username secret ID"
  type        = string
}

variable "keycloak_db_password_secret_id" {
  description = "Keycloak DB password secret ID"
  type        = string
}

variable "allowed_hosts" {
  description = "Allowed hosts for the API server"
  type        = string
  default     = "*"
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