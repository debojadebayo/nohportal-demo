output subnets_ids {
    description = "List of subnet IDs"
    value  = [for subnet in azurerm_subnet.subnets : subnet.id]
}

output vnet_id {
    description = "Virtual network ID"
    value = azurerm_virtual_network.vnet.id
}