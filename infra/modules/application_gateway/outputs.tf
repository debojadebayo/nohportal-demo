output "gateway_fqdn" {
  value = "${azurerm_public_ip.app_gateway_ip.domain_name_label}.${var.location}.cloudapp.azure.com"
}
output "gateway_public_ip" {
  value = azurerm_public_ip.app_gateway_ip.ip_address
}