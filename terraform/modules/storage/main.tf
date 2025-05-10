# Storage Account for Blob Storage
resource "azurerm_storage_account" "main" {
  name                     = var.storage_account_name
  resource_group_name      = var.resource_group_name
  location                 = var.location
  account_tier             = "Standard"
  account_replication_type = "GRS"  # Geo-redundant storage for production
  account_kind             = "StorageV2"
  access_tier              = "Hot"
  min_tls_version          = "TLS1_2"
  
  # Network rules to restrict access
  network_rules {
    default_action             = "Deny"
    virtual_network_subnet_ids = [var.subnet_ids["data"], var.subnet_ids["backend"]]
    bypass                     = ["AzureServices"]
  }
  
  # Blob properties
  blob_properties {
    versioning_enabled       = true
    change_feed_enabled      = true
    default_service_version  = "2020-06-12"
    
    # Soft delete for blobs
    delete_retention_policy {
      days = 30
    }
    
    # Soft delete for containers
    container_delete_retention_policy {
      days = 30
    }
  }
  
  # Prevent accidental deletion in production
  # lifecycle {
  #   prevent_destroy = true
  # }
}

# Create storage containers for different purposes
resource "azurerm_storage_container" "app_data" {
  name                  = "app-data"
  storage_account_id    = azurerm_storage_account.main.id
  container_access_type = "private"
  
  metadata = {
    "data_classification" = "phi"  # Protected Health Information
    "compliance_regime"   = "hipaa"
    "department"          = "clinical"
    "purpose"             = "patient_documents"
    "encryption_key"      = "key_vault_managed"
    "key_vault_reference" = var.key_vault_id
  }
}

resource "azurerm_storage_container" "backups" {
  name                  = "backups"
  storage_account_id = azurerm_storage_account.main.id
  container_access_type = "private"
  
  metadata = {
    "data_classification" = "system"
    "compliance_regime"   = "hipaa"
    "department"          = "it"
    "purpose"             = "database_backups"
    "retention_policy"    = "30_days"
    "encryption_key"      = "key_vault_managed"
    "key_vault_reference" = var.key_vault_id
  }
}

resource "azurerm_storage_container" "media" {
  name                  = "media"
  storage_account_id = azurerm_storage_account.main.id
  container_access_type = "private"
  
  metadata = {
    "data_classification" = "phi"  # Protected Health Information
    "compliance_regime"   = "hipaa"
    "department"          = "clinical"
    "purpose"             = "medical_imaging"
    "encryption_key"      = "key_vault_managed"
    "key_vault_reference" = var.key_vault_id
  }
}

# Private endpoint for secure access
resource "azurerm_private_endpoint" "storage" {
  name                = "${var.storage_account_name}-endpoint"
  location            = var.location
  resource_group_name = var.resource_group_name
  subnet_id           = var.subnet_ids["data"]

  private_service_connection {
    name                           = "${var.storage_account_name}-connection"
    private_connection_resource_id = azurerm_storage_account.main.id
    is_manual_connection           = false
    subresource_names              = ["blob"]
  }

  private_dns_zone_group {
    name                 = "storage-dns-zone-group"
    private_dns_zone_ids = [azurerm_private_dns_zone.blob.id]
  }
}

# Private DNS Zone for Blob Storage
resource "azurerm_private_dns_zone" "blob" {
  name                = "privatelink.blob.core.windows.net"
  resource_group_name = var.resource_group_name
}

# Link the DNS Zone to the VNet
resource "azurerm_private_dns_zone_virtual_network_link" "blob" {
  name                  = "blob-dns-link"
  resource_group_name   = var.resource_group_name
  private_dns_zone_name = azurerm_private_dns_zone.blob.name
  virtual_network_id    = var.vnet_id
}