variable "location" {
  description = "Location for the resources"
  type        = string
  default     = "ukwest"
}

variable "vnet_name" {
  description = "Virtual network name"
  type        = string
  default     = "nationoh-vnet"
}

variable "resource_group_name" {
  description = "Name of the resource group"
  type        = string
}

variable "address_space" {
  description = "Address space for the virtual network"
  type        = list(string)
  default     = ["10.0.0.0/16"]
}

variable "subnet_names" {
  description = "List of subnet names"
  type        = list(string)
  default     = ["appgateway", "backend", "data", "privatelink"]
}

variable "subnet_prefixes" {
  description = "CIDR blocks for each subnet"
  type        = list(string)
  default     = ["10.0.1.0/24", "10.0.2.0/23", "10.0.3.0/24", "10.0.4.0/24"]
}

variable "nsg_name" {
  description = "Network security group name"
  type        = string
  default     = "nationoh-nsg"
}

variable "nsg_rules" {
  description = "map of network security rules for each subnet"
  type = map(list(object({
    name                       = string
    priority                   = number
    direction                  = string
    access                     = string
    protocol                   = string
    source_port_range          = string
    destination_port_range     = string
    source_address_prefix      = string
    destination_address_prefix = string
  })))
  default = {
    appgateway = [
      {
        name                       = "allow-app-gateway-management"
        priority                   = 140
        direction                  = "Inbound"
        access                     = "Allow"
        protocol                   = "Tcp"
        source_port_range          = "*"
        destination_port_range     = "65200-65535"
        source_address_prefix      = "GatewayManager"
        destination_address_prefix = "*"
      },
      {
        name                       = "Allow-HTTP"
        priority                   = 110
        direction                  = "Inbound"
        access                     = "Allow"
        protocol                   = "Tcp"
        source_port_range          = "*"
        destination_port_range     = "80"
        source_address_prefix      = "Internet"
        destination_address_prefix = "*"
      },
      {
        name                       = "Allow-HTTPS"
        priority                   = 120
        direction                  = "Inbound"
        access                     = "Allow"
        protocol                   = "Tcp"
        source_port_range          = "*"
        destination_port_range     = "443"
        source_address_prefix      = "Internet"
        destination_address_prefix = "*"
      },
      {
        name                       = "allow-outbound-to-backend"
        priority                   = 130
        direction                  = "Outbound"
        access                     = "Allow"
        protocol                   = "Tcp"
        source_port_range          = "*"
        destination_port_range     = "5003"
        source_address_prefix      = "*"
        destination_address_prefix = "10.0.2.0/23"
      }
    ],
    backend = [
      {
        name                       = "allow-api-from-app-gateway"
        priority                   = 100
        direction                  = "Inbound"
        access                     = "Allow"
        protocol                   = "Tcp"
        source_port_range          = "*"
        destination_port_range     = "5003"
        source_address_prefix      = "10.0.1.0/24"
        destination_address_prefix = "*"
      },
      {
        name                       = "allow-keycloak"
        priority                   = 110
        direction                  = "Inbound"
        access                     = "Allow"
        protocol                   = "Tcp"
        source_port_range          = "*"
        destination_port_range     = "8180"
        source_address_prefix      = "10.0.1.0/23"
        destination_address_prefix = "*"
      },
      {
        name                       = "allow-inbound-from-db"
        priority                   = 130
        direction                  = "Inbound"
        access                     = "Allow"
        protocol                   = "Tcp"
        source_port_range          = "*"
        destination_port_range     = "5435"
        source_address_prefix      = "10.0.3.0/24"
        destination_address_prefix = "10.0.2.0/23"
      }
    ],
    data = [
      {
        name                       = "allow-app-postgres"
        priority                   = 100
        direction                  = "Inbound"
        access                     = "Allow"
        protocol                   = "Tcp"
        source_port_range          = "*"
        destination_port_range     = "5435"
        source_address_prefix      = "10.0.2.0/23"
        destination_address_prefix = "*"
      },
      {
        name                       = "allow-keycloak-postgres"
        priority                   = 110
        direction                  = "Inbound"
        access                     = "Allow"
        protocol                   = "Tcp"
        source_port_range          = "*"
        destination_port_range     = "5434"
        source_address_prefix      = "10.0.2.0/23"
        destination_address_prefix = "*"
      }
    ]
  }
}