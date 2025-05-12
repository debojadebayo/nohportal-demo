output "instrumentation_key" {
  description = "Instrumentation key for Application Insights"
  value       = azurerm_application_insights.insights.instrumentation_key
  sensitive   = true
}

output "app_id" {
  description = "App ID for Application Insights"
  value       = azurerm_application_insights.insights.app_id
}

output "connection_string" {
  description = "Connection string for Application Insights"
  value       = azurerm_application_insights.insights.connection_string
  sensitive   = true
}

output "log_analytics_workspace_id" {
  description = "ID of the Log Analytics workspace"
  value       = azurerm_log_analytics_workspace.workspace.id
}

output "log_analytics_workspace_name" {
  description = "Name of the Log Analytics workspace"
  value       = azurerm_log_analytics_workspace.workspace.name
}