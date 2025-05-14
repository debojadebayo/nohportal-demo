variable "location" {
  description = "Azure region where resources will be created"
  type        = string
}

variable "resource_group_name" {
  description = "Name of the resource group"
  type        = string
}

variable "user_assigned_identity_id" {
  description = "User Assigned Managed Identity ID for Keycloak"
  type        = string
}

variable "container_env_name" {
  description = "Name of the Container App Environment"
  type        = string
  default     = "nationoh-env"
}

variable "subnet_id" {
  description = "ID of the subnet"
  type        = string
}

variable "aspnetcore_environment" {
  description = "ASPNETCORE_ENVIRONMENT value"
  type        = string
  default     = "Development"
}

variable "cpu" {
  description = "CPU for the container"
  type        = number
  default     = 0.5
}

variable "memory" {
  description = "Memory for the container"
  type        = string
  default     = "1Gi"
}


variable "container_registry_url" {
  description = "Container registry URL"
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

variable "log_analytics_workspace_name" {
  description = "Name of the Log Analytics workspace"
  type        = string
}

variable "keycloak_db_username" {
  description = "Keycloak DB username"
  type        = string
  default     = "keycloak"
}

# variable "keycloak_db_password" {
#   description = "Keycloak DB password"
#   type        = string
#   sensitive   = true
#   default     = "dev-db-password"
# }

# variable "keycloak_db_name" {
#   description = "Keycloak DB name"
#   type        = string
#   default     = "keycloak"
# }

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

variable "keycloak_features" {
  description = "Keycloak features to enable"
  type        = string
  default     = "organization"
}

variable "keycloak_db_url" {
  description = "Keycloak DB JDBC URL"
  type        = string
  # default     = "jdbc:postgresql://${var.postgresql_server_fqdn}:5432/${var.keycloak_db_name}"
}


# variable "image_tags" {
#   description = "Map of image tags for each container"
#   type        = map(string)
#   default = {
#     server   = "latest"
#     frontend = "latest"
#     keycloak = "latest"
#   }
# }

# variable "container_app_server_name" {
#   description = "Name of the container app server"
#   type        = string
# }

# variable "container_app_urls" {
#   description = "URLs for the container apps"
#   type        = map(string)
#   default     = {}
# }

# variable "log_analytics_workspace_id" {
#   description = "ID of the Log Analytics workspace for container app logs"
#   type        = string
#   default     = null
# }

# variable "domain_name" {
#   description = "domain names of the application"
#   type        = string
# }

# variable "postgresql_server_fqdn" {
#   description = "FQDN of the PostgreSQL flexible server"
#   type        = string
# }

# variable "keycloak_db_name" {
#   description = "Name of the Keycloak database"
#   type        = string
# }