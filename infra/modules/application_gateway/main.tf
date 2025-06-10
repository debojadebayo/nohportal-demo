resource "azurerm_public_ip" "app_gateway_ip" {
  name                = "${var.resource_group_name}-app-gateway-ip"
  location            = var.location
  resource_group_name = var.resource_group_name
  allocation_method   = "Static"
  sku                 = "Standard"
  domain_name_label   = "${var.resource_group_name}-gateway"
}

resource "azurerm_web_application_firewall_policy" "waf_policy" {
  name                = "${var.resource_group_name}-waf-policy"
  location            = var.location
  resource_group_name = var.resource_group_name

  policy_settings {
    enabled                     = true
    mode                        = "Prevention"
    request_body_check          = true
    file_upload_limit_in_mb     = 100
    max_request_body_size_in_kb = 128
  }

  managed_rules {
    managed_rule_set {
      type    = "OWASP"
      version = "3.2"

      rule_group_override {
        rule_group_name = "REQUEST-942-APPLICATION-ATTACK-SQLI"
        rule {
          id      = "942100"
          enabled = true
          action  = "Block"
        }
      }
    }
  }

  custom_rules {
    name      = "BlockHighRiskCountries"
    priority  = 10
    rule_type = "MatchRule"
    action    = "Block"
    match_conditions {
      match_variables {
        variable_name = "RemoteAddr"
      }
      operator           = "GeoMatch"
      negation_condition = false
      match_values       = ["RU", "CN", "IR", "KP"]
    }
  }
}

resource "azurerm_application_gateway" "app_gateway" {
  name                = "NationOHAppGateway"
  location            = var.location
  resource_group_name = var.resource_group_name
  sku {
    name     = var.app_gateway_sku_tier
    tier     = var.app_gateway_sku_tier
    capacity = 2
  }

  gateway_ip_configuration {
    name      = "gateway-ip-config"
    subnet_id = var.subnet_id
  }

  frontend_port {
    name = "http-port"
    port = 80
  }

  frontend_port {
    name = "https-port"
    port = 443
  }

  frontend_ip_configuration {
    name                 = "frontend-ip-config"
    public_ip_address_id = azurerm_public_ip.app_gateway_ip.id
  }

  backend_address_pool {
    name  = "frontend-backend-pool"
    fqdns = length(var.frontend_fqdn) > 0 ? [var.frontend_fqdn] : []
  }

  backend_address_pool {
    name  = "api-backend-pool"
    fqdns = length(var.api_fqdn) > 0 ? [var.api_fqdn] : []
  }

  backend_address_pool {
    name  = "auth-backend-pool"
    fqdns = length(var.auth_fqdn) > 0 ? [var.auth_fqdn] : []
  }

  dynamic "ssl_certificate" {
    for_each = length(var.ssl_certificate_path) > 0 ? [1] : []
    content {
      name     = "nationoh-cert"
      data     = filebase64(var.ssl_certificate_path)
      password = var.ssl_certificate_password
    }
  }

  #   Http to https redirect 
  dynamic "redirect_configuration" {
    for_each = length(var.ssl_certificate_path) > 0 ? [1] : []
    content {
      name                 = "http-to-https"
      redirect_type        = "Permanent"
      target_listener_name = "https-listener"
      include_path         = true
      include_query_string = true
    }
  }

  backend_http_settings {
    name                                = "frontend-http-settings"
    cookie_based_affinity               = "Disabled"
    port                                = 5002
    protocol                            = "Http"
    request_timeout                     = 60
    probe_name                          = "frontend-health-probe"
    pick_host_name_from_backend_address = true
  }

  backend_http_settings {
    name                                = "api-http-settings"
    cookie_based_affinity               = "Disabled"
    port                                = 5003
    protocol                            = "Http"
    request_timeout                     = 60
    probe_name                          = "api-health-probe"
    pick_host_name_from_backend_address = true
  }

  backend_http_settings {
    name                                = "auth-http-settings"
    cookie_based_affinity               = "Enabled"
    port                                = 8080
    protocol                            = "Http"
    request_timeout                     = 60
    probe_name                          = "auth-health-probe"
    pick_host_name_from_backend_address = true
  }

  # Health probes
  probe {
    name = "frontend-health-probe"
    # host                = var.frontend_fqdn
    protocol                                  = "Http"
    path                                      = "/health"
    interval                                  = 30
    timeout                                   = 30
    unhealthy_threshold                       = 3
    pick_host_name_from_backend_http_settings = true
  }

  probe {
    name = "api-health-probe"
    # host                = var.api_fqdn
    protocol                                  = "Http"
    path                                      = "/api-health"
    interval                                  = 30
    timeout                                   = 30
    unhealthy_threshold                       = 3
    pick_host_name_from_backend_http_settings = true
  }

  probe {
    name = "auth-health-probe"
    # host                = var.auth_fqdn
    protocol                                  = "Http"
    path                                      = "/auth/realms/master"
    interval                                  = 30
    timeout                                   = 30
    unhealthy_threshold                       = 3
    pick_host_name_from_backend_http_settings = true
  }

  #   HTTP listener
  http_listener {
    name                           = "http-listener"
    frontend_ip_configuration_name = "frontend-ip-config"
    frontend_port_name             = "http-port"
    protocol                       = "Http"
    host_name                      = "${azurerm_public_ip.app_gateway_ip.domain_name_label}.${var.location}.cloudapp.azure.com"
  }

  #   HTTPS listener
  dynamic "http_listener" {
    for_each = length(var.ssl_certificate_path) > 0 ? [1] : []
    content {
      name                           = "https-listener"
      frontend_ip_configuration_name = "frontend-ip-config"
      frontend_port_name             = "https-port"
      protocol                       = "Https"
      ssl_certificate_name           = "nationoh-cert"
      host_name                      = "${azurerm_public_ip.app_gateway_ip.domain_name_label}.${var.location}.cloudapp.azure.com"
    }
  }

  # URL path map for path-based routing
  url_path_map {
    name                               = "path-based-routing"
    default_backend_address_pool_name  = "frontend-backend-pool"
    default_backend_http_settings_name = "frontend-http-settings"

    path_rule {
      name                       = "api-rule"
      paths                      = ["/api/*"]
      backend_address_pool_name  = "api-backend-pool"
      backend_http_settings_name = "api-http-settings"

    }

    path_rule {
      name                       = "auth-rule"
      paths                      = ["/auth/*"]
      backend_address_pool_name  = "auth-backend-pool"
      backend_http_settings_name = "auth-http-settings"
    }
  }

  # Routing rules
  request_routing_rule {
    name                        = "default-routing-rule"
    rule_type                   = "Basic"
    http_listener_name          = "http-listener"
    backend_address_pool_name   = length(var.ssl_certificate_path) > 0 ? null : "frontend-backend-pool"
    backend_http_settings_name  = length(var.ssl_certificate_path) > 0 ? null : "frontend-http-settings"
    redirect_configuration_name = length(var.ssl_certificate_path) > 0 ? "http-to-https" : null
    priority                    = 10
  }

  dynamic "request_routing_rule" {
    for_each = length(var.ssl_certificate_path) > 0 ? [1] : []
    content {
      name               = "https-routing-rule"
      rule_type          = "PathBasedRouting"
      http_listener_name = "https-listener"
      url_path_map_name  = "path-based-routing"
      priority           = 20
    }
  }

  firewall_policy_id = azurerm_web_application_firewall_policy.waf_policy.id
}