variable "key_vault_name" {
    description = "Key vault name"
    type = string
}

variable "location" {
    description = "Location for the resources"
    type = string
}

variable "resource_group_name" {
    description = "Name of the resource group"
    type = string
}

variable "subnet_ids" {
    description = "Map of subnet names to subnet IDs"
    type = map(string)
}

variable "vnet_id" {
    description = "Virtual network ID"
    type = string
}
    
variable "db_admin_username" {
    description = "Database admin username"
    type = string
}

variable "db_admin_password" {
    description = "Database admin password"
    type = string
}

variable "storage_primary_access_key" {
    description = "Storage account primary access key"
    type = string
}

variable "ssl_certificate_password" {
    description = "SSL certificate password"
    type = string
}

variable "keycloak_db_username" {
    description = "Keycloak database username"
    type = string
}

variable "keycloak_db_password" {
    description = "Keycloak database password"
    type = string
}

variable "keycloak_admin_username" {
    description = "Keycloak admin username"
    type = string
}

variable "keycloak_admin_password" {
    description = "Keycloak admin password"
    type = string
}

variable "patient_db_username" {
    description = "Patient database username"
    type = string
}

variable "patient_db_password" {
    description = "Patient database password"
    type = string
}

variable "jwt_signing_key" {
    description = "JWT signing key for secure token validation"
    type = string
    default = ""  # Will be generated if not provided
}