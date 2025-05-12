deboadebayo@rmhroot terraform % terraform validate 

╷
│ Error: Unsupported block type
│ 
│   on modules/networking/main.tf line 40, in resource "azurerm_subnet" "subnets":
│   40:   dynamic "service_endpoints" {
│ 
│ Blocks of type "service_endpoints" are not expected here.
╵
╷
│ Error: Reference to undeclared resource
│ 
│   on modules/networking/main.tf line 53, in resource "azurerm_network_security_group" "nsg":
│   53:   resource_group_name = azurerm_resource_group.rg.name
│ 
│ A managed resource "azurerm_resource_group" "rg" has not been declared in module.networking.