$nodeResourceGroup = (az aks show --resource-group aksdemo --name aksdemo --query nodeResourceGroup -o tsv)
az network public-ip create --resource-group $nodeResourceGroup --name aksdemoPublicIP --sku Standard --allocation-method static --query publicIp.ipAddress -o tsv