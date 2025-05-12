variable "location" {
  description = "Location for the resources"
  type        = string
}

variable "resource_group_name" {
  description = "Name of the resource group"
  type        = string
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
    keycloak = "26.1"
  }
}

variable "domain_name" {
  description = "Base domain name for the application"
  type        = string
  # default = "nationoh.co.uk" 
}

variable "container_app_urls" {
  description = "URLs for the deployed container apps"
  type        = map(string)
}

variable "db_admin_username" {
  description = "Database admin username"
  type        = string
}

variable "db_admin_password" {
  description = "Database admin password"
  type        = string
}

variable "container_subnet_cidr_start" {
  description = "Container subnet CIDR start"
  type        = string
}

variable "container_subnet_cidr_end" {
  description = "Container subnet CIDR end"
  type        = string
}

variable "vnet_id" {
  description = "Virtual network ID"
  type        = string
}

variable "postgres_dns_zone_name" {
  description = "The name of the private DNS zone for PostgreSQL"
  type        = string
}

variable "postgresql_server_name" {
  description = "Name of the PostgreSQL Flexible Server"
  type        = string
  default     = null # Let Azure generate a name if not specified
}

variable "private_dns_zone_name" {
  description = "Name of the private DNS zone for PostgreSQL"
  type        = string
  default     = null # Let Azure generate a name if not specified
}

variable "dns_link_name" {
  description = "Name of the DNS zone to VNet link"
  type        = string
  default     = null # Let Azure generate a name if not specified
}

variable "app_database_name" {
  description = "Name of the application database"
  type        = string
  default     = null # Let Azure generate a name if not specified
}

variable "keycloak_database_name" {
  description = "Name of the Keycloak database"
  type        = string
  default     = null # Let Azure generate a name if not specified
}

variable "firewall_rule_name" {
  description = "Name of the PostgreSQL firewall rule"
  type        = string
  default     = null # Let Azure generate a name if not specified
}
    