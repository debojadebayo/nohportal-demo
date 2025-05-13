# # Azure Container Registry
# resource "azurerm_container_registry" "acr" {
#   name                = var.registry_name
#   resource_group_name = var.resource_group_name
#   location            = var.location
#   sku                 = "Premium"
#   admin_enabled       = false

#   # Network rules to restrict access
#   network_rule_set {
#     default_action = "Deny"

#     ip_rule {
#       action   = "Allow"
#       ip_range = var.allowed_ip_range
#     }

#     virtual_network {
#       action    = "Allow"
#       subnet_id = var.subnet_ids["backend"]
#     }
#   }

#   # Enable features for healthcare compliance
#   encryption {
#     key_vault_key_id   = var.key_vault_key_id
#     identity_client_id = azurerm_user_assigned_identity.acr_identity.client_id
#   }

#   # Geo-replication for disaster recovery
#   georeplications {
#     location                = var.geo_replica_location
#     zone_redundancy_enabled = true
#   }

#   # Content trust for image signing
#   trust_policy {
#     enabled = true
#   }
# }

# # Managed identity for ACR
# resource "azurerm_user_assigned_identity" "acr_identity" {
#   name                = "${var.registry_name}-identity"
#   resource_group_name = var.resource_group_name
#   location            = var.location
# }

# # Private endpoint for secure access
# resource "azurerm_private_endpoint" "acr" {
#   name                = "${var.registry_name}-endpoint"
#   location            = var.location
#   resource_group_name = var.resource_group_name
#   subnet_id           = var.subnet_ids["data"]

#   private_service_connection {
#     name                           = "${var.registry_name}-connection"
#     private_connection_resource_id = azurerm_container_registry.acr.id
#     is_manual_connection           = false
#     subresource_names              = ["registry"]
#   }

#   private_dns_zone_group {
#     name                 = "acr-dns-zone-group"
#     private_dns_zone_ids = [azurerm_private_dns_zone.acr.id]
#   }
# }

# # Private DNS Zone for ACR
# resource "azurerm_private_dns_zone" "acr" {
#   name                = "privatelink.azurecr.io"
#   resource_group_name = var.resource_group_name
# }

# # Link the DNS Zone to the VNet
# resource "azurerm_private_dns_zone_virtual_network_link" "acr" {
#   name                  = "acr-dns-link"
#   resource_group_name   = var.resource_group_name
#   private_dns_zone_name = azurerm_private_dns_zone.acr.name
#   virtual_network_id    = var.vnet_id
# }