$REGISTRY_NAME="jakob"
$SOURCE_REGISTRY="k8s.gcr.io"
$CONTROLLER_IMAGE="ingress-nginx/controller"
$CONTROLLER_TAG="v1.0.4"
$PATCH_IMAGE="ingress-nginx/kube-webhook-certgen"
$PATCH_TAG="v1.1.1"
$DEFAULTBACKEND_IMAGE="defaultbackend-amd64"
$DEFAULTBACKEND_TAG="1.5"
$CERT_MANAGER_REGISTRY="quay.io"
$CERT_MANAGER_TAG="v1.5.4"
$CERT_MANAGER_IMAGE_CONTROLLER="jetstack/cert-manager-controller"
$CERT_MANAGER_IMAGE_WEBHOOK="jetstack/cert-manager-webhook"
$CERT_MANAGER_IMAGE_CAINJECTOR="jetstack/cert-manager-cainjector"

# Set variable for ACR location to use for pulling images
$ACR_URL="jakob.azurecr.io"
$STATIC_IP="137.117.184.68"
$DNS_LABEL="aksdemo"


# Label the cert-manager namespace to disable resource validation
kubectl label namespace default cert-manager.io/disable-validation=true

# Add the Jetstack Helm repository
helm repo add jetstack https://charts.jetstack.io

# Update your local Helm chart repository cache
helm repo update


helm upgrade --install cert-manager jetstack/cert-manager `
  --version $CERT_MANAGER_TAG `
  --set installCRDs=true `
  --set nodeSelector."kubernetes\.io/os"=linux `
  --set image.repository=$ACR_URL/$CERT_MANAGER_IMAGE_CONTROLLER `
  --set image.tag=$CERT_MANAGER_TAG `
  --set webhook.image.repository=$ACR_URL/$CERT_MANAGER_IMAGE_WEBHOOK `
  --set webhook.image.tag=$CERT_MANAGER_TAG `
  --set cainjector.image.repository=$ACR_URL/$CERT_MANAGER_IMAGE_CAINJECTOR `
  --set cainjector.image.tag=$CERT_MANAGER_TAG