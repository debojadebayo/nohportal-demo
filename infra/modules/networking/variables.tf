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
  default     = ["appgateway", "backend", "data", "privatelink", "vm"]
}

variable "subnet_prefixes" {
  description = "CIDR blocks for each subnet"
  type        = list(string)
  default     = ["10.0.0.0/24", "10.0.2.0/23", "10.0.4.0/24", "10.0.5.0/24", "10.0.6.0/24"]
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
        name                       = "Allow-HTTP"
        priority                   = 100
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
        name                       = "allow-outbound-to-container-apps"
        priority                   = 130
        direction                  = "Outbound"
        access                     = "Allow"
        protocol                   = "Tcp"
        source_port_range          = "*"
        destination_port_range     = "8080"
        source_address_prefix      = "*"
        destination_address_prefix = "10.0.2.0/23"
      },
      {
        name                       = "allow-outbound-to-container-apps-https"
        priority                   = 140
        direction                  = "Outbound"
        access                     = "Allow"
        protocol                   = "Tcp"
        source_port_range          = "*"
        destination_port_range     = "443"
        source_address_prefix      = "*"
        destination_address_prefix = "10.0.2.0/23"
      }
    ],
    backend = [
      # Allow appgateway to reach containers 
      {
        name                       = "allow-api-from-app-gateway-http"
        priority                   = 100
        direction                  = "Inbound"
        access                     = "Allow"
        protocol                   = "Tcp"
        source_port_range          = "*"
        destination_port_range     = "8080"
        source_address_prefix      = "10.0.0.0/24"
        destination_address_prefix = "*"
      },
      {
        name                       = "allow-api-from-app-gateway-https"
        priority                   = 110
        direction                  = "Inbound"
        access                     = "Allow"
        protocol                   = "Tcp"
        source_port_range          = "*"
        destination_port_range     = "443"
        source_address_prefix      = "10.0.0.0/24"
        destination_address_prefix = "*"
      },
      {
        name                       = "allow-internal-container-apps"
        priority                   = 120
        direction                  = "Inbound"
        access                     = "Allow"
        protocol                   = "Tcp"
        source_port_range          = "*"
        destination_port_range     = "8080"
        source_address_prefix      = "10.0.2.0/23"
        destination_address_prefix = "*"
      },
      {
        name                       = "allow-outbound-to-postgres"
        priority                   = 200
        direction                  = "Outbound"
        access                     = "Allow"
        protocol                   = "Tcp"
        source_port_range          = "*"
        destination_port_range     = "5432"
        source_address_prefix      = "*"
        destination_address_prefix = "10.0.4.0/24"
      },
      {
        name                       = "allow-outbound-to-privatelink"
        priority                   = 201
        direction                  = "Outbound"
        access                     = "Allow"
        protocol                   = "Tcp"
        source_port_range          = "*"
        destination_port_range     = "5432"
        source_address_prefix      = "*"
        destination_address_prefix = "10.0.5.0/24"
      },
      {
        name                       = "allow-outbound-internet"
        priority                   = 210
        direction                  = "Outbound"
        access                     = "Allow"
        protocol                   = "*"
        source_port_range          = "*"
        destination_port_range     = "*"
        source_address_prefix      = "*"
        destination_address_prefix = "Internet"
      },
      {
        name                       = "allow-outbound-vnet"
        priority                   = 220
        direction                  = "Outbound"
        access                     = "Allow"
        protocol                   = "*"
        source_port_range          = "*"
        destination_port_range     = "*"
        source_address_prefix      = "*"
        destination_address_prefix = "10.0.0.0/16"
      }
    ],
    data = [
      {
        name                       = "allow-postgres-from-container-apps"
        priority                   = 100
        direction                  = "Inbound"
        access                     = "Allow"
        protocol                   = "Tcp"
        source_port_range          = "*"
        destination_port_range     = "5432"
        source_address_prefix      = "10.0.2.0/23" # backend subnet
        destination_address_prefix = "*"
      }
    ],
    privatelink = [
      # Private endpoints for PostgreSQL
      {
        name                       = "allow-postgres-private-endpoint"
        priority                   = 100
        direction                  = "Inbound"
        access                     = "Allow"
        protocol                   = "Tcp"
        source_port_range          = "*"
        destination_port_range     = "5432"
        source_address_prefix      = "10.0.2.0/23"
        destination_address_prefix = "*"
      }
    ],
    vm = [
      {
        name                       = "allow-ssh"
        priority                   = 100
        direction                  = "Inbound"
        access                     = "Allow"
        protocol                   = "Tcp"
        source_port_range          = "*"
        destination_port_range     = "22"
        source_address_prefix      = "Internet"
        destination_address_prefix = "*"
      },
      {
        name                       = "allow-outbound-to-backend"
        priority                   = 110
        direction                  = "Outbound"
        access                     = "Allow"
        protocol                   = "Tcp"
        source_port_range          = "*"
        destination_port_range     = "8080"
        source_address_prefix      = "*"
        destination_address_prefix = "10.0.2.0/23"
      }
    ]
  }
}