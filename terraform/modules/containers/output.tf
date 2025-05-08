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