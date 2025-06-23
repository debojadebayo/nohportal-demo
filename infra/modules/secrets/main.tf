resource "azurerm_key_vault" "kv" {
  name                        = var.key_vault_name
  location                    = var.location
  resource_group_name         = var.resource_group_name
  enabled_for_disk_encryption = true
  tenant_id                   = data.azurerm_client_config.current.tenant_id
  soft_delete_retention_days  = 90
  purge_protection_enabled    = true
  sku_name                    = "standard"

  # Network ACLs for restricting access
  network_acls {
    default_action             = "Allow" # After initial deployment, change this back to "Deny" for production
    bypass                     = "AzureServices"
    virtual_network_subnet_ids = [var.subnet_ids["backend"], var.subnet_ids["data"]]
  }

  # access policy  

  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = data.azurerm_client_config.current.object_id

    key_permissions = [
      "Get",
      "List",
      "Create",
      "Delete",
      "Update",
      "Recover",
      "Purge",
      "GetRotationPolicy",
      "SetRotationPolicy"
    ]
    secret_permissions = [
      "Get",
      "List",
      "Set",
      "Delete",
      "Recover",
      "Backup",
      "Restore",
      "Purge"
    ]

    certificate_permissions = [
      "Get",
      "List",
      "Create",
      "Import",
      "Update",
      "Delete",
      "Recover",
      "Backup",
      "Restore",
      "Purge"
    ]
  }

  # Add access policy for Keycloak Managed Identity
  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = var.keycloak_managed_identity_object_id

    secret_permissions = [
      "Get",
      "List",
      "Set",
      "Delete",
      "Recover",
      "Backup",
      "Restore",
      "Purge"
    ]
  }

  # Add access policy for GitHub Actions service principal (when provided)
  dynamic "access_policy" {
    for_each = var.github_actions_service_principal_object_id != "" ? [1] : []
    content {
      tenant_id = data.azurerm_client_config.current.tenant_id
      object_id = var.github_actions_service_principal_object_id

      secret_permissions = [
        "Get",
        "List",
        "Set"
      ]
    }
  }

  # Prevent accidental deletion in production
  # lifecycle {
  #   prevent_destroy = true
  # }

}

# Get current Azure client config 
data "azurerm_client_config" "current" {}

# Private endpoint for secure access
resource "azurerm_private_endpoint" "keyvault" {
  name                = "${var.key_vault_name}-endpoint"
  location            = var.location
  resource_group_name = var.resource_group_name
  subnet_id           = var.subnet_ids["privatelink"]

  private_service_connection {
    name                           = "${var.key_vault_name}-connection"
    private_connection_resource_id = azurerm_key_vault.kv.id
    is_manual_connection           = false
    subresource_names              = ["vault"]
  }

  private_dns_zone_group {
    name                 = "keyvault-dns-zone-group"
    private_dns_zone_ids = [azurerm_private_dns_zone.keyvault.id]
  }
}

# Private DNS Zone for Key Vault
resource "azurerm_private_dns_zone" "keyvault" {
  name                = "privatelink.vaultcore.azure.net"
  resource_group_name = var.resource_group_name
}

# Link the DNS Zone to the VNet
resource "azurerm_private_dns_zone_virtual_network_link" "keyvault" {
  name                  = "keyvault-dns-link"
  resource_group_name   = var.resource_group_name
  private_dns_zone_name = azurerm_private_dns_zone.keyvault.name
  virtual_network_id    = var.vnet_id
}

# # Storage encryption key
# resource "azurerm_key_vault_key" "storage_key" {
#   name         = "storage-encryption-key"
#   key_vault_id = azurerm_key_vault.kv.id
#   key_type     = "RSA"
#   key_size     = 2048

#   key_opts = [
#     "decrypt",
#     "encrypt",
#     "sign",
#     "unwrapKey",
#     "verify",
#     "wrapKey",
#   ]

#   rotation_policy {
#     automatic {
#       time_before_expiry = "P30D" # 30 days before expiry
#     }

#     expire_after         = "P90D" # 90 days
#     notify_before_expiry = "P29D" # 29 days before expiry
#   }
# }

# Secrets for the application

# Database admin credentials

resource "random_string" "postgresql_admin_username_prefix" {
  length  = 1
  special = false
  numeric = false
  upper   = false
}

resource "random_string" "postgresql_admin_username_suffix" {
  length  = 23
  special = false
  upper   = false
}

resource "azurerm_key_vault_secret" "postgresql_admin_username" {
  name         = "postgresql-admin-username"
  value        = "${random_string.postgresql_admin_username_prefix.result}${random_string.postgresql_admin_username_suffix.result}"
  key_vault_id = azurerm_key_vault.kv.id
}

resource "random_password" "postgresql_admin_password" {
  length  = 24
  special = true
}

resource "azurerm_key_vault_secret" "postgresql_admin_password" {
  name         = "postgresql-admin-password"
  value        = random_password.postgresql_admin_password.result
  key_vault_id = azurerm_key_vault.kv.id
}

# Keycloak admin credentials

resource "random_string" "keycloak_admin_username_prefix" {
  length  = 1
  special = false
  numeric = false
  upper   = false
}

resource "random_string" "keycloak_admin_username_suffix" {
  length  = 23
  special = false
  upper   = false
}

resource "azurerm_key_vault_secret" "keycloak_admin_username" {
  name         = "keycloak-admin-username"
  value        = "${random_string.keycloak_admin_username_prefix.result}${random_string.keycloak_admin_username_suffix.result}"
  key_vault_id = azurerm_key_vault.kv.id
}

resource "random_password" "keycloak_admin_password" {
  length  = 24
  special = true
}

resource "azurerm_key_vault_secret" "keycloak_admin_password" {
  name         = "keycloak-admin-password"
  value        = random_password.keycloak_admin_password.result
  key_vault_id = azurerm_key_vault.kv.id
}

resource "random_password" "keycloak_db_username" {
  length  = 24
  special = true
}

resource "azurerm_key_vault_secret" "keycloak_db_username" {
  name         = "keycloak-db-username"
  value        = random_password.keycloak_db_username.result
  key_vault_id = azurerm_key_vault.kv.id
}

resource "random_password" "keycloak_db_password" {
  length  = 24
  special = true
}

resource "azurerm_key_vault_secret" "keycloak_db_password" {
  name         = "keycloak-db-password"
  value        = random_password.keycloak_db_password.result
  key_vault_id = azurerm_key_vault.kv.id
}

# # SSL certificate password
# resource "azurerm_key_vault_secret" "ssl_certificate_password" {
#   count        = var.ssl_certificate_password != "" ? 1 : 0
#   name         = "ssl-certificate-password"
#   value        = var.ssl_certificate_password
#   key_vault_id = azurerm_key_vault.kv.id
# }

# # Keycloak database credentials
# resource "azurerm_key_vault_secret" "keycloak_db_username" {
#   count        = var.keycloak_db_username != "" ? 1 : 0
#   name         = "keycloak-db-username"
#   value        = var.keycloak_db_username
#   key_vault_id = azurerm_key_vault.kv.id
# }

# resource "azurerm_key_vault_secret" "keycloak_db_password" {
#   count        = var.keycloak_db_password != "" ? 1 : 0
#   name         = "keycloak-db-password"
#   value        = var.keycloak_db_password
#   key_vault_id = azurerm_key_vault.kv.id
# }

# # Patient database application credentials
# resource "azurerm_key_vault_secret" "patient_db_username" {
#   count        = var.patient_db_username != "" ? 1 : 0
#   name         = "patient-db-username"
#   value        = var.patient_db_username
#   key_vault_id = azurerm_key_vault.kv.id
# }

# resource "azurerm_key_vault_secret" "patient_db_password" {
#   count        = var.patient_db_password != "" ? 1 : 0
#   name         = "patient-db-password"
#   value        = var.patient_db_password
#   key_vault_id = azurerm_key_vault.kv.id
# }

# resource "azurerm_key_vault_secret" "jwt_signing_key" {
#   count        = var.jwt_signing_key != "" ? 1 : 0
#   name         = "jwt-signing-key"
#   value        = var.jwt_signing_key
#   key_vault_id = azurerm_key_vault.kv.id
# }
