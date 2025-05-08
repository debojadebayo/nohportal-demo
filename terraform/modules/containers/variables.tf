variable "container_env_name" {
    description = "Container environment name"
    type = string
    default = "ComposedHealth-container-env"
}

variable "location" {
    description = "Location for the resources"
    type = string
    default = "eu-west-1"
}

variable "resource_group_name" {
    description = "Name of the resource group"
    type = string
}

variable "subnet_ids" {
    description = "Map of subnet names to subnet IDs"
    type = map(string)
}