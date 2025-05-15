output "gateway_fqdn" {
  value = azurerm_public_ip.app_gateway_ip.fqdn
}
output "gateway_public_ip" {
  value = azurerm_public_ip.app_gateway_ip.ip_address
}