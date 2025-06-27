# Production Environment Variables - Consolidated Infrastructure

# Basic Configuration
resource_group_name = "nationohprod"
location            = "uksouth"
environment         = "prod"

# GitHub Actions Service Principal Object ID for Key Vault access (replace with actual production value)
github_actions_service_principal_object_id = "REPLACE_WITH_PROD_SERVICE_PRINCIPAL_OBJECT_ID"

# Container Configuration
container_cpu          = 1.0
container_memory       = "2Gi"
aspnetcore_environment = "Production"

# Container Images (applying commit 244a9e0 changes - using actual images instead of nginx)
server_image   = "nationohprodregistry.azurecr.io/app.server:prod-latest"
frontend_image = "nationohprodregistry.azurecr.io/app.client:prod-latest"

# Allowed Hosts
allowed_hosts = "app.nationoh.co.uk,www.nationoh.co.uk"

# Image Tags
image_tags = {
  keycloak = "26.1"
}

# Application Gateway Configuration
app_gateway_sku_tier = "WAF_v2"

# Domain and SSL Configuration
domain_name              = ""
ssl_certificate_path     = ""
ssl_certificate_password = ""