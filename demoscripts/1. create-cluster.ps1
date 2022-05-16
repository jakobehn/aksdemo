$resourceGroup = "aksdemo"
$clusterName = "aksdemo"

"Create resource group"
az group create --name $resourceGroup --location westeurope

"Create log analytics workspace"
az monitor log-analytics workspace create `
    --resource-group $resourceGroup `
    --workspace-name $resourceGroup

$logAnalyticsWorkspaceId = az monitor log-analytics workspace show -n $resourceGroup -g $resourceGroup --query id

"Create vnet"
az network vnet create `
    --resource-group $resourceGroup `
    --name aksdemo `
    --address-prefixes 10.0.0.0/8 `
    --subnet-name aksdemosubnet `
    --subnet-prefix 10.240.0.0/16

"Create ACI subnet"
az network vnet subnet create `
    --resource-group $resourceGroup `
    --vnet-name aksdemo `
    --name aci `
    --address-prefixes 10.241.0.0/16

$vnetId = az network vnet show --resource-group $resourceGroup --name aksdemo --query id -o tsv
$subnetId = az network vnet subnet show --resource-group $resourceGroup --vnet-name aksdemo --name aksdemosubnet --query id -o tsv

"Create AKS cluster"
az aks create `
    --resource-group $resourceGroup `
    --name $clusterName `
    --enable-managed-identity `
    --vm-set-type VirtualMachineScaleSets `
    --node-count 3 `
    --generate-ssh-keys `
    --load-balancer-sku standard `
    --attach-acr jakob `
    --enable-addons monitoring,virtual-node `
    --workspace-resource-id $logAnalyticsWorkspaceId `
    --zones 1 2 3 `
    --enable-cluster-autoscaler `
    --min-count 3 `
    --max-count 6 `
    --network-plugin azure `
    --aci-subnet-name aci `
    --vnet-subnet-id $subnetId

"Get cluster credentials"
az aks get-credentials -n $clusterName -g $resourceGroup