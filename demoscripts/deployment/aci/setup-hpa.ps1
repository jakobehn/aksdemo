kubectl autoscale deployment frontend --namespace qbox --cpu-percent=10 --min=1 --max=10