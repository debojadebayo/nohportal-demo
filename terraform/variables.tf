variable "app_name" {
  type        = string
  description = "name of the app"
  default     = "nohportaldemoapp"
}

variable "location" {
  type        = string
  description = "Location of Resources"
  default     = "uksouth"
}

variable "environment" {
  type        = string
  description = "Environment"
  default     = "Development"
}

variable "GITHUB_SHA" {
  type        = string
  description = "The commit SHA that triggered the workflow"
}

variable "postgres_host" {
  type        = string
  description = "PostgreSQL server hostname"
  default     = "nohportaldemo-postgres.postgres.database.azure.com"
}


variable "postgres_user" {
  type        = string
  description = "PostgreSQL username"
  default     = "postgres"
}

variable "postgres_password" {
  type        = string
  description = "PostgreSQL password"
  sensitive   = true
  default     = "123"
}

variable "postgres_db" {
  type        = string
  description = "PostgreSQL database name"
  default     = "postgres"
}