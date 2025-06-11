# NationOH Azure Deployment Guide

This guide provides step-by-step instructions for securely deploying the NationOH application to Azure using Infrastructure as Code (Terraform) and automated CI/CD pipelines.

## 📋 Overview

The NationOH application deploys a complete healthcare management solution with:

- **Frontend**: Blazor WebAssembly client served via nginx
- **Backend**: .NET 9 Web API with Entity Framework Core
- **Database**: PostgreSQL with separate databases for application and Keycloak
- **Authentication**: Keycloak identity provider
- **Infrastructure**: Azure Container Apps with private networking, Application Gateway with WAF

## 🏗️ Architecture

```
Azure Application Gateway (WAF) 
    ↓
Azure Container Apps Environment
    ├── Frontend Container App (Blazor/nginx)
    ├── Backend Container App (.NET API)
    └── Keycloak Container App (Authentication)
    ↓
Azure Database for PostgreSQL (Private Endpoint)
Azure Key Vault (Secrets Management)
Azure Container Registry (Private)
Azure Storage Account (Blob Storage)
```

## 🔐 Prerequisites

### Azure Requirements

1. **Azure Subscription** with the following permissions:
   - `Contributor` or `Owner` role on the subscription
   - Ability to create resource groups and assign roles

2. **Azure CLI** installed and authenticated:
   ```bash
   az login
   az account set --subscription "your-subscription-id"
   ```

3. **Tools Required**:
   - Azure CLI (v2.40+)
   - Git
   - GitHub account with repository access

### Required Azure Resource Providers

The following providers must be registered before deploying. **This is a one-time setup per subscription.**

#### Manual Registration Required

Run these commands to register all required providers:

```bash
# Essential providers for this deployment
az provider register --namespace Microsoft.App
az provider register --namespace Microsoft.ContainerRegistry
az provider register --namespace Microsoft.DBforPostgreSQL
az provider register --namespace Microsoft.KeyVault
az provider register --namespace Microsoft.Network
az provider register --namespace Microsoft.Storage
az provider register --namespace Microsoft.OperationalInsights

# Check registration status (providers must show "Registered")
az provider show --namespace Microsoft.App --query "registrationState" --output tsv
az provider show --namespace Microsoft.ContainerRegistry --query "registrationState" --output tsv
az provider show --namespace Microsoft.DBforPostgreSQL --query "registrationState" --output tsv
az provider show --namespace Microsoft.KeyVault --query "registrationState" --output tsv
az provider show --namespace Microsoft.Network --query "registrationState" --output tsv
az provider show --namespace Microsoft.Storage --query "registrationState" --output tsv
az provider show --namespace Microsoft.OperationalInsights --query "registrationState" --output tsv
```

#### Wait for Registration

Provider registration typically takes 1-2 minutes. Wait for all providers to show "Registered" status before proceeding with Terraform deployment.

#### Why This is Required

- Azure subscriptions don't automatically register all resource providers
- First-time usage of services like Container Apps requires manual registration
- Registration failures cause Terraform deployment errors with "MissingSubscriptionRegistration" messages

## 🚀 Deployment Process

### Step 1: Register Azure Resource Providers

**IMPORTANT: Complete this step before running Terraform!**

```bash
# Register all required providers (run all commands)
az provider register --namespace Microsoft.App
az provider register --namespace Microsoft.ContainerRegistry
az provider register --namespace Microsoft.DBforPostgreSQL
az provider register --namespace Microsoft.KeyVault
az provider register --namespace Microsoft.Network
az provider register --namespace Microsoft.Storage
az provider register --namespace Microsoft.OperationalInsights

# Verify all are registered (wait for "Registered" status)
az provider show --namespace Microsoft.App --query "registrationState"
```

### Step 2: Bootstrap Terraform Backend

Create the Terraform state storage resources manually (one-time setup):

```bash
# Set your subscription ID
SUBSCRIPTION_ID="your-subscription-id-here"
az account set --subscription "$SUBSCRIPTION_ID"

# Create resource group for Terraform state
az group create \
  --name "terraform-state-rg-latest" \
  --location "uksouth"

# Create storage account for Terraform state
az storage account create \
  --name "nationohtfstatelatest" \
  --resource-group "terraform-state-rg-latest" \
  --location "uksouth" \
  --sku "Standard_LRS"

# Create storage container
az storage container create \
  --name "tfstatelatest" \
  --account-name "nationohtfstatelatest"
```

### ⚠️ CRITICAL: Terraform State Management

**NEVER mix local and remote Terraform state!** This configuration uses **remote state storage** for CI/CD consistency.

#### Local Development Rules

If you need to run Terraform locally for testing:

```bash
cd infra

# ALWAYS clean local state first
rm -f terraform.tfstate*
rm -f *.tfplan
rm -rf .terraform/

# Re-initialize to connect to remote backend
terraform init

# Now you can plan/apply safely
terraform plan -var-file="dev.tfvars"
```

#### What NOT to Do

❌ **Never run Terraform locally without cleaning state first**  
❌ **Never commit `.tfstate` files to git**  
❌ **Never run `terraform init` without remote backend configured**

#### Why This Matters

- **Local state conflicts** with remote state cause deployment failures
- **Resource name/location mismatches** indicate state conflicts
- **GitHub Actions and local runs** must use the same state source

### Step 3: Configure GitHub Secrets

Create the following secrets in your GitHub repository (`Settings` → `Secrets and variables` → `Actions`):

#### Required Secrets

1. **AZURE_CREDENTIALS**: Service Principal for GitHub Actions
   ```bash
   # Create service principal
   az ad sp create-for-rbac \
     --name "nationoh-github-actions" \
     --role "Contributor" \
     --scopes "/subscriptions/$SUBSCRIPTION_ID" \
     --sdk-auth
   ```
   Copy the entire JSON output to the `AZURE_CREDENTIALS` secret.

### Step 4: Environment Configuration

Choose your deployment environment and verify the configuration:

#### Development Environment (`devops-setup` or `master` branch)
- Resource Group: `nohdevfresh`
- Location: `uksouth`
- Container Resources: 0.5 CPU, 1Gi memory
- Environment: Development

#### Production Environment (`production` branch)
- Resource Group: `nationohprod`
- Location: `uksouth`
- Container Resources: 1.0 CPU, 2Gi memory
- Environment: Production

### Step 5: Deploy Infrastructure

The deployment uses automated GitHub Actions workflows:

1. **Infrastructure Workflow**: Deploys Azure resources using Terraform
2. **Build Workflow**: Builds and pushes container images to ACR
3. **Deploy Workflow**: Updates Container Apps with new images

#### Automatic Deployment (Recommended)

1. **Push to trigger workflows**:
   ```bash
   git push origin devops-setup  # For dev environment
   # OR
   git push origin production    # For prod environment
   ```

2. **Monitor workflow execution** in GitHub Actions tab

3. **Workflow sequence**:
   - Infrastructure → Build → Deploy (runs automatically)

#### Manual Deployment

You can also trigger workflows manually:

1. Go to `Actions` tab in GitHub
2. Select the workflow you want to run
3. Click `Run workflow`
4. Choose the environment (dev/prod)

### Step 6: Verification

After deployment completes, verify the application:

1. **Get application URLs**:
   ```bash
   # Set resource group based on environment
   RESOURCE_GROUP="nohdevfresh"  # or "nationohprod" for production
   
   # Get frontend URL
   az containerapp show \
     --name "${RESOURCE_GROUP}frontend" \
     --resource-group "$RESOURCE_GROUP" \
     --query "properties.configuration.ingress.fqdn" \
     --output tsv
   
   # Get API URL
   az containerapp show \
     --name "${RESOURCE_GROUP}server" \
     --resource-group "$RESOURCE_GROUP" \
     --query "properties.configuration.ingress.fqdn" \
     --output tsv
   ```

2. **Test endpoints**:
   - Frontend: `https://your-frontend-url`
   - API Health: `https://your-api-url/health`
   - Keycloak: `https://your-api-url/auth`

## 🔒 Security Considerations

### Access Control

- **Managed Identities**: Container Apps use managed identities for ACR access
- **Private Networking**: All resources communicate over private endpoints
- **Key Vault**: Secrets are stored in Azure Key Vault with RBAC
- **WAF Protection**: Application Gateway provides Web Application Firewall

### Network Security

- **VNet Integration**: All services run within a private virtual network
- **Private Endpoints**: Database and storage use private endpoints only
- **NSG Rules**: Network Security Groups restrict traffic flow

### Secrets Management

- Database credentials stored in Key Vault
- Keycloak admin credentials stored in Key Vault
- Container Apps reference secrets via managed identity

## 🛠️ Troubleshooting

### Common Issues

1. **Terraform Backend Access Denied**
   ```bash
   # Check if bootstrap resources exist
   az storage account show --name "nationohtfstatelatest" --resource-group "terraform-state-rg-latest"
   ```

2. **ACR Authentication Failures**
   - Verify managed identity has `AcrPull` role on registry
   - Check container app identity configuration

3. **Database Connection Issues**
   - Verify private endpoint configuration
   - Check firewall rules and network access

4. **Keycloak Startup Issues**
   - Check Key Vault access for managed identity
   - Verify database connectivity

5. **Terraform State Conflicts**
   ```bash
   # Symptoms: Resource name/location mismatches, "already exists" errors
   # Solution: Clean local state and re-initialize
   cd infra
   rm -f terraform.tfstate*
   rm -f *.tfplan  
   rm -rf .terraform/
   terraform init
   terraform plan -var-file="dev.tfvars"
   ```

6. **Key Vault Permission Errors**
   - **Error**: `does not have secrets get permission on key vault`
   - **Cause**: GitHub Actions service principal missing Key Vault access
   - **Solution**: Verify `github_actions_service_principal_object_id` in `dev.tfvars`

### Workflow Failures

1. **Infrastructure Workflow**:
   - Check Azure credentials and permissions
   - Verify subscription ID in `main.tf`
   - Ensure resource providers are registered

2. **Build Workflow**:
   - Check ACR access and login
   - Verify Docker build context and files

3. **Deploy Workflow**:
   - Check container app exists and is accessible
   - Verify image tags and registry URLs

### Logs and Monitoring

```bash
# View container app logs
az containerapp logs show \
  --name "your-container-app" \
  --resource-group "your-resource-group" \
  --follow

# View recent deployments
az containerapp revision list \
  --name "your-container-app" \
  --resource-group "your-resource-group"
```

## 📦 Infrastructure Components

### Core Resources

- **Resource Group**: Container for all resources
- **Virtual Network**: Private network with subnets for different tiers
- **Container Apps Environment**: Managed platform for containers
- **Application Gateway**: Load balancer with WAF capabilities
- **Azure Database for PostgreSQL**: Managed database service
- **Azure Container Registry**: Private container image storage
- **Azure Key Vault**: Centralized secrets management
- **Storage Account**: Blob storage for application data

### Networking

- **Application Gateway Subnet**: Public-facing load balancer
- **Container Apps Subnet**: Private subnet for application containers
- **Database Subnet**: Private subnet for PostgreSQL
- **Private Endpoints**: Secure access to PaaS services

## 🔄 Maintenance

### Updates and Patches

1. **Application Updates**: Push code changes to trigger automatic builds
2. **Infrastructure Changes**: Modify Terraform files and push to trigger infrastructure updates
3. **Security Updates**: Container base images updated automatically via rebuild

### Scaling

- **Horizontal Scaling**: Configure auto-scaling rules in Container Apps
- **Vertical Scaling**: Adjust CPU/memory allocation in `dev.tfvars`/`prod.tfvars`
- **Database Scaling**: Scale PostgreSQL compute and storage as needed

### Backup and Recovery

- **Database**: Automated backups enabled on PostgreSQL
- **Application Data**: Stored in Azure Storage with geo-redundancy
- **Configuration**: Infrastructure as Code enables rapid recreation

## 📞 Support

For deployment issues:

1. Check GitHub Actions workflow logs
2. Review Azure resource logs via Azure Portal
3. Verify configuration in `infra/` directory
4. Consult troubleshooting section above

---

**Last Updated**: January 2025  
**Version**: 1.0  
**Supported Environments**: Development, Production