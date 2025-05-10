# Application Insights for monitoring
resource "azurerm_application_insights" "insights" {
  name                = var.insights_name
  location            = var.location
  resource_group_name = var.resource_group_name
  application_type    = "web"
  
  # Healthcare-specific settings
  retention_in_days   = 90  # Retain data for compliance
  sampling_percentage = 100 # Full data collection for healthcare auditing
  
  # Disable IP masking for full audit trail
  internet_ingestion_enabled = true
  internet_query_enabled     = true
  
  # Tags for healthcare compliance
  tags = {
    environment = "production"
    compliance  = "hipaa"
    data_type   = "application_telemetry"
  }
}

# Store the instrumentation key in Key Vault
resource "azurerm_key_vault_secret" "app_insights_key" {
  name         = "app-insights-key"
  value        = azurerm_application_insights.insights.instrumentation_key
  key_vault_id = var.key_vault_id
}

# Log Analytics workspace for advanced analytics
resource "azurerm_log_analytics_workspace" "workspace" {
  name                = "${var.insights_name}-workspace"
  location            = var.location
  resource_group_name = var.resource_group_name
  sku                 = "PerGB2018"
  retention_in_days   = 90  # Match Application Insights retention
}

# Link Application Insights to Log Analytics
resource "azurerm_application_insights_analytics_item" "query_pack" {
  name                    = "Healthcare-Queries"
  application_insights_id = azurerm_application_insights.insights.id
  content                 = <<QUERY
// Sample healthcare monitoring queries
// Patient data access audit
requests
| where url contains "patient" or url contains "medical"
| project timestamp, url, success, user_Id, client_IP
| order by timestamp desc

// Error monitoring for critical services
exceptions
| where cloud_RoleName in ("PatientPortal", "MedicalRecords")
| summarize count() by type, cloud_RoleName
| order by count_ desc

// Performance monitoring
requests
| where success == "True"
| summarize avg(duration), percentile(duration, 95), percentile(duration, 99) by name
| order by percentile_duration_99 desc
QUERY
  type                    = "query"
  scope                   = "shared"
}

# Alert for potential data breaches
resource "azurerm_monitor_scheduled_query_rules_alert" "data_access_alert" {
  name                = "patient-data-access-alert"
  location            = var.location
  resource_group_name = var.resource_group_name
  
  action {
    action_group           = [azurerm_monitor_action_group.security_team.id]
    email_subject          = "Potential unauthorized patient data access"
    custom_webhook_payload = "{\"alert\":\"Potential unauthorized patient data access\",\"severity\":\"High\"}"
  }
  
  data_source_id = azurerm_application_insights.insights.id
  description    = "Alert when there is potential unauthorized access to patient data"
  enabled        = true
  
  query       = <<-QUERY
requests
| where url contains "patient" or url contains "medical"
| where client_IP !in (${join(",", var.authorized_ip_ranges)})
| count
  QUERY
  severity    = 1
  frequency   = 5
  time_window = 5
  
  trigger {
    operator  = "GreaterThan"
    threshold = 0
  }
}

# Security team action group
resource "azurerm_monitor_action_group" "security_team" {
  name                = "security-team"
  resource_group_name = var.resource_group_name
  short_name          = "security"
  
  email_receiver {
    name                    = "security-team"
    email_address           = var.security_email
    use_common_alert_schema = true
  }
}