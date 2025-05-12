output "subnets_ids" {
  description = "Map of subnet names to subnet IDs"
  value       = { for subnet_name, subnet in azurerm_subnet.subnets : subnet_name => subnet.id }
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

output "postgres_dns_zone_name" {
  description = "Name of the PostgreSQL private DNS zone"
  value       = "nationoh.postgres.database.azure.com"
}

output "container_app_urls" {
  description = "URLs for the deployed container apps"
  value       = var.container_app_urls
}