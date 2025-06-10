# Dev variables 

resource_group_name = "nationohdevjun2025"
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