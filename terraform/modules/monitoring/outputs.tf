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