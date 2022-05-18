docker compose build

docker tag qboxcoreweb jakob.azurecr.io/qboxcoreweb:1.0
docker tag qboxcoreapi jakob.azurecr.io/qboxcoreapi:1.0

docker push jakob.azurecr.io/qboxcoreweb:1.0
docker psuh jakob.azurecr.io/qboxcoreapi:1.0