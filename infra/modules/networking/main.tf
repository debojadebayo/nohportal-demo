
# Virtual Network
resource "azurerm_virtual_network" "vnet" {
  name                = var.vnet_name
  address_space       = var.address_space
  location            = var.location
  resource_group_name = var.resource_group_name
}

# Subnets
resource "azurerm_subnet" "subnets" {
  for_each             = toset(var.subnet_names)
  name                 = each.value
  resource_group_name  = var.resource_group_name
  virtual_network_name = azurerm_virtual_network.vnet.name
  address_prefixes     = [var.subnet_prefixes[index(var.subnet_names, each.value)]]

  dynamic "delegation" {
    for_each = each.value == "backend" ? [1] : []
    content {
      name = "container-apps-delegation"
      service_delegation {
        name    = "Microsoft.App/environments"
        actions = ["Microsoft.Network/virtualNetworks/subnets/join/action"]
      }
    }
  }

  dynamic "delegation" {
    for_each = each.value == "data" ? [1] : []
    content {
      name = "postgres-delegation"
      service_delegation {
        name    = "Microsoft.DBforPostgreSQL/flexibleServers"
        actions = [
          "Microsoft.Network/virtualNetworks/subnets/join/action",
          "Microsoft.Network/virtualNetworks/subnets/prepareNetworkPolicies/action",
          "Microsoft.Network/virtualNetworks/subnets/unprepareNetworkPolicies/action"
        ]
      }
    }
  }

  service_endpoints = each.value == "data" ? ["Microsoft.Storage"] : []
}

# Network Security Group

resource "azurerm_network_security_group" "nsg" {
  for_each            = toset(var.subnet_names)
  name                = "${each.value}-nsg"
  resource_group_name = var.resource_group_name
  location            = var.location
}

resource "azurerm_subnet_network_security_group_association" "association" {
  for_each                  = toset(var.subnet_names)
  subnet_id                 = azurerm_subnet.subnets[each.value].id
  network_security_group_id = azurerm_network_security_group.nsg[each.value].id
}


# Network Security Rules

resource "azurerm_network_security_rule" "rule" {
  for_each = {
    for rule in local.nsg_rules_flat : "${rule.subnet_name}-${rule.rule_name}" => rule
  }

  name                        = each.value.rule_name
  priority                    = each.value.priority
  direction                   = each.value.direction
  access                      = each.value.access
  protocol                    = each.value.protocol
  source_port_range           = each.value.source_port_range
  destination_port_range      = each.value.destination_port_range
  source_address_prefix       = each.value.source_address_prefix
  destination_address_prefix  = each.value.destination_address_prefix
  resource_group_name         = var.resource_group_name
  network_security_group_name = azurerm_network_security_group.nsg[each.value.subnet_name].name
}

locals {
  nsg_rules_flat = flatten([
    for subnet_name, rules in var.nsg_rules : [
      for rule_index, rule in rules : {
        subnet_name                = subnet_name
        rule_name                  = rule.name
        priority                   = rule.priority
        direction                  = rule.direction
        access                     = rule.access
        protocol                   = rule.protocol
        source_port_range          = rule.source_port_range
        destination_port_range     = rule.destination_port_range
        source_address_prefix      = rule.source_address_prefix
        destination_address_prefix = rule.destination_address_prefix
      }
    ]
  ])
}