variable "location" {
  description = "Location for the resources"
  type        = string
}

variable "resource_group_name" {
  description = "Name of the resource group"
  type        = string
}

variable "insights_name" {
  description = "Name of the Application Insights instance"
  type        = string
}

variable "key_vault_id" {
  description = "ID of the Key Vault"
  type        = string
}

variable "authorized_ip_ranges" {
  description = "List of authorized IP ranges for patient data access"
  type        = list(string)
  default     = ["10.0.0.0/8"]  # Default to internal network
}

variable "security_email" {
  description = "Email address for security alerts"
  type        = string
  default     = "security@composedhealth.com"
}