az containerapp create --name "containertam" \
  --resource-group "nohportaldemo-rg" \
  --environment "nohportaldemoacaenv" \
  --user-assigned "/subscriptions/072589a3-320c-46e5-82f8-71773bb13809/resourcegroups/nohportaldemo-rg/providers/Microsoft.ManagedIdentity/userAssignedIdentities/containerappmi" \
  --registry-identity "/subscriptions/072589a3-320c-46e5-82f8-71773bb13809/resourcegroups/nohportaldemo-rg/providers/Microsoft.ManagedIdentity/userAssignedIdentities/containerappmi" \
  --registry-server "nohportaldemoacracr.azurecr.io" \
  --image "nohportaldemoacracr.azurecr.io/app.client:latest"