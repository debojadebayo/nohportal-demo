output "instrumentation_key" {
  description = "Instrumentation key for Application Insights"
  value       = azurerm_application_insights.insights.instrumentation_key
  sensitive   = true
}

output "app_id" {
  description = "App ID for Application Insights"
  value       = azurerm_application_insights.insights.app_id
}


output "log_analytics_workspace_id" {
  description = "ID of the Log Analytics workspace"
  value       = azurerm_log_analytics_workspace.log_analytics.id
}