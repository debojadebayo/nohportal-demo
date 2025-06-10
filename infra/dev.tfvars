# Dev variables 

resource_group_name = "nohdevjun2025"
location            = "uksouth"
domain_name         = ""
# will need to add a domain main

# ASPNETCORE
aspnetcore_environment = "Development"

# Container
container_cpu    = 0.5
container_memory = "1Gi"

# Application Gateway
app_gateway_sku_tier = "WAF_v2"

# GitHub Actions Service Principal Object ID for Key Vault access
github_actions_service_principal_object_id = "4e4da53e-e85a-430f-9bcf-168ca0d53bc6"