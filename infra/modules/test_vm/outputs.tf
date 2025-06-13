output "vm_public_ip" {
  description = "Public IP address of the test VM"
  value       = azurerm_public_ip.vm_public_ip.ip_address
}

output "vm_private_ip" {
  description = "Private IP address of the test VM"
  value       = azurerm_network_interface.vm_nic.private_ip_address
}

output "vm_fqdn" {
  description = "FQDN of the test VM public IP"
  value       = azurerm_public_ip.vm_public_ip.fqdn
}

output "ssh_command" {
  description = "SSH command to connect to the VM"
  value       = "ssh ${var.admin_username}@${azurerm_public_ip.vm_public_ip.ip_address}"
}

output "ssh_private_key" {
  description = "Private SSH key for VM access (save this to a file)"
  value       = tls_private_key.vm_ssh_key.private_key_pem
  sensitive   = true
}