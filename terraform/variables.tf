variable "location" {
    description = "Location for the resources"
    type = string
    default = "eu-west-1"
}

variable "resource_group_name" {
    description = "Name of the resource group"
    type = string
    default = "ComposedHealth-rg"
}

variable "container_registry" {
    description = "Container registry URL"
    type = string
}

variable "image_tags" {
    description = "Map of image tags for each container"
    type = map(string)
    default = {
        server   = "latest"
        frontend = "latest"
        keycloak = "26.1"
    }
}

variable "domain_name" {
    description = "Base domain name for the application"
    type = string
    # default = "composedhealth.com"
}