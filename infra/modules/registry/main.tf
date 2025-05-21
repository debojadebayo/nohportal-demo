# Azure Container Registry
resource "azurerm_container_registry" "acr" {
  name                = var.registry_name
  resource_group_name = var.resource_group_name
  location            = var.location
  sku                 = "Premium"
  admin_enabled       = true

  # Network rules to restrict access
  network_rule_set {
    default_action = "Allow" # After initial deployment, change this back to "Deny" for production

    # When switching to "Deny" default action, uncomment and configure these rules
    # ip_rule {
    #   action   = "Allow"
    #   ip_range = var.allowed_ip_range
    # }
  }

  # Geo-replication for disaster recovery
  georeplications {
    location                = var.geo_replica_location
    zone_redundancy_enabled = true
  }

}

# Managed identity for ACR
resource "azurerm_user_assigned_identity" "acr_identity" {
  name                = "${var.registry_name}-identity"
  resource_group_name = var.resource_group_name
  location            = var.location
}

# Private endpoint for secure access
resource "azurerm_private_endpoint" "acr" {
  name                = var.acr_endpoint_name
  location            = var.location
  resource_group_name = var.resource_group_name
  subnet_id           = var.subnet_ids["privatelink"]

  private_service_connection {
    name                           = var.acr_connection_name
    private_connection_resource_id = azurerm_container_registry.acr.id
    is_manual_connection           = false
    subresource_names              = ["registry"]
  }

  private_dns_zone_group {
    name                 = var.acr_dns_zone_group_name
    private_dns_zone_ids = [azurerm_private_dns_zone.acr.id]
  }
}

# Private DNS Zone for ACR
resource "azurerm_private_dns_zone" "acr" {
  name                = "${var.acr_dns_zone_name}.azurecr.io"
  resource_group_name = var.resource_group_name
}

# Link the DNS Zone to the VNet
resource "azurerm_private_dns_zone_virtual_network_link" "acr" {
  name                  = var.acr_dns_link_name
  resource_group_name   = var.resource_group_name
  private_dns_zone_name = azurerm_private_dns_zone.acr.name
  virtual_network_id    = var.vnet_id
}