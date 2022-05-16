az aks enable-addons `
    --resource-group aksdemo `
    --name ndc-aksdemo `
    --addons virtual-node `
    --subnet-name aci