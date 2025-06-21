# Development Environment Variables - Consolidated Infrastructure

# Basic Configuration
resource_group_name = "nohdevjun2025"
location            = "uksouth"
environment         = "dev"

# GitHub Actions Service Principal Object ID for Key Vault access
github_actions_service_principal_object_id = "4e4da53e-e85a-430f-9bcf-168ca0d53bc6"

# Container Configuration
container_cpu          = 0.5
container_memory       = "1Gi"
aspnetcore_environment = "Development"

# Container Images (applying commit 244a9e0 changes - using actual images instead of nginx)
server_image   = "nohdevjun2025registry.azurecr.io/app.server:dev-latest"
frontend_image = "nohdevjun2025registry.azurecr.io/app.client:dev-latest"

# Allowed Hosts
allowed_hosts = "*"

# Image Tags
image_tags = {
  keycloak = "26.1"
}

# Application Gateway Configuration
app_gateway_sku_tier = "WAF_v2"

# Domain and SSL Configuration (empty for dev)
domain_name              = ""
ssl_certificate_path     = ""
ssl_certificate_password = ""