# Prod variables 

resource_group_name = "nationohprod"
location = "ukwest"
# domain_name = "" Official production domain name - app.nationoh.co.uk

# ASPNETCORE
aspnetcore_environment = "Production"

# Container
container_cpu = 1.0
container_memory = "2Gi"

# Application Gateway
app_gateway_sku_tier = "WAF_v2"