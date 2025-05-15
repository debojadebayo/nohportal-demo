output "subnets_ids" {
  description = "List of subnet IDs"
  value       = {
    for subnet in azurerm_subnet.subnets : subnet.name => subnet.id
  }
}

output "vnet_id" {
  description = "Virtual network ID"
  value       = azurerm_virtual_network.vnet.id
}

output "container_subnet_cidr_start" {
  description = "Start IP address of the container subnet CIDR block"
  value       = cidrhost(azurerm_subnet.subnets["backend"].address_prefixes[0], 0)
}

output "container_subnet_cidr_end" {
  description = "End IP address of the container subnet CIDR block"
  value       = cidrhost(azurerm_subnet.subnets["backend"].address_prefixes[0], 255)
}
