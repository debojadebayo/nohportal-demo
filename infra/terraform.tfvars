# Required basic variables
resource_group_name = "nationoh-rg"
location = "eu-west-1"
domain_name = "dev-nationoh.co.uk"

# Authentication/security variables
db_admin_username = "dbadmin"
db_admin_password = "123!" 
keycloak_admin_username = "keycloakadmin"
keycloak_admin_password = "456!"  
jwt_signing_key = "your-signing-key"  

# SSL certificate info
ssl_certificate_password = "CertPassword789!"
ssl_certificate_path = "./path/to/your/certificate.pfx"  # Path to your SSL certificate file

# Container registry
container_registry = "nationohregistry"  # This will be used in naming

# Container image tags (example - adjust based on your images)
image_tags = {
  api = "latest"
  frontend = "latest"
  keycloak = "latest"
  # Add other services
}