$ACR_NAME="jakob"
$SERVICE_PRINCIPAL_NAME="acr-service-principal"

# Populate the ACR login server and resource id.
$ACR_LOGIN_SERVER=(az acr show --name $ACR_NAME --query loginServer --output tsv)
$ACR_REGISTRY_ID=(az acr show --name $ACR_NAME --query id --output tsv)

# Create acrpull role assignment with a scope of the ACR resource.
$SP_PASSWD=(az ad sp create-for-rbac --name "http://$($SERVICE_PRINCIPAL_NAME)" --role acrpull --scopes $ACR_REGISTRY_ID --query password --output tsv)

$SP_APP_ID = "bd48cc19-2c4e-4737-a27f-1ba8ea81f001"
# Get the service principal client id.
$CLIENT_ID=(az ad sp show --id $SP_APP_ID --query appId --output tsv)

# Output used when creating Kubernetes secret.
$CLIENT_ID
$SP_PASSWD

kubectl create secret docker-registry acr-auth --docker-server $ACR_LOGIN_SERVER --docker-username $CLIENT_ID --docker-password $SP_PASSWD --docker-email jakob@ehn.nu