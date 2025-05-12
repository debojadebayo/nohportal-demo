variable "location" {
  description = "Location for the resources"
  type        = string
  default     = "eu-west-1"
}

variable "resource_group_name" {
  description = "Name of the resource group"
  type        = string
  default     = "nationoh-rg"
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
  default = "https://nationoh.co.uk"
}

variable "ssl_certificate_password" {
  description = "Password for the SSL certificate"
  type        = string
}

variable "ssl_certificate_path" {
  description = "Path to the SSL certificate"
  type        = string
}

variable "db_admin_username" {
  description = "Database admin username"
  type        = string
}

variable "db_admin_password" {
  description = "Database admin password"
  type        = string
}

variable "storage_primary_access_key" {
  description = "Primary access key for the storage account"
  type        = string
}

variable "vnet_id" {
  description = "Virtual network ID"
  type        = string
}

variable "subnet_ids" {
  description = "Map of subnet IDs for different purposes"
  type        = map(string)
}

variable "key_vault_name" {
  description = "Key Vault name"
  type        = string
}

variable "key_vault_id" {
  description = "Key Vault ID"
  type        = string
}

variable "keycloak_admin_password" {
  description = "Keycloak admin password"
  type        = string
}

variable "keycloak_admin_username" {
  description = "Keycloak admin username"
  type        = string
}

variable "keycloak_db_username" {
  description = "Keycloak database username"
  type        = string
}
  
variable "keycloak_db_password" {
  description = "Keycloak database password"
  type        = string
}

variable "keycloak_db_name" {
  description = "Keycloak database name"
  type        = string
}

variable "patient_db_username" {
  description = "Patient database username"
  type        = string
}

variable "patient_db_password" {
  description = "Patient database password"
  type        = string
}

variable "jwt_signing_key" {
  description = "JWT signing key"
  type        = string
}



  



    