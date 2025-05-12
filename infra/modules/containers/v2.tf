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
  default     = "ComposedHealth-env"
}

variable "subnet_ids" {
  description = "Map of subnet IDs"
  type        = map(string)
}

variable "container_registry" {
  description = "Container registry URL"
  type        = string
}

variable "image_tags" {
  description = "Map of image tags for each container"
  type        = map(string)
  default = {
    server   = "latest"
    frontend = "latest"
    keycloak = "latest"
  }
}

variable "container_app_urls" {
  description = "URLs for the container apps"
  type        = map(string)
  default     = {}
}

variable "log_analytics_workspace_id" {
  description = "ID of the Log Analytics workspace for container app logs"
  type        = string
  default     = null
}

variable "domain_name" {
  description = "domain names of the application"
  type        = string
}