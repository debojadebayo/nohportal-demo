resource "azurerm_application_insights" "insights" {
  name                = var.insights_name
  location            = var.location
  resource_group_name = var.resource_group_name
  application_type    = "web"
  workspace_id        = azurerm_log_analytics_workspace.log_analytics.id

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

resource "azurerm_key_vault_secret" "app_insights_key" {
  name         = "app-insights-key"
  value        = azurerm_application_insights.insights.instrumentation_key
  key_vault_id = var.key_vault_id
}

resource "azurerm_log_analytics_workspace" "log_analytics" {
  name                = "${var.insights_name}-workspace"
  location            = var.location
  resource_group_name = var.resource_group_name
  retention_in_days   = 90
  
  tags = {
    environment = "production"
    compliance  = "hipaa"
  }
}