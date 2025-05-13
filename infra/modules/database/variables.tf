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

variable "postgresql_server_name" {
  description = "Name of the PostgreSQL Flexible Server"
  type        = string
}

variable "private_dns_zone_name" {
  description = "Name of the private DNS zone for PostgreSQL"
  type        = string
}

variable "dns_link_name" {
  description = "Name of the DNS zone to VNet link"
  type        = string
}

variable "app_database_name" {
  description = "Name of the application database"
  type        = string
}

variable "keycloak_db_name" {
  description = "Name of the Keycloak database"
  type        = string
}

variable "firewall_rule_name" {
  description = "Name of the PostgreSQL firewall rule"
  type        = string
}
    