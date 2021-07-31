# Url Shortening Key Generation Service

In order to build docker image, run the below command from the same directory as this README.md.

```shell
docker image build -f UrlShorteningKeyGenerationService/Dockerfile -t url-shortening-key-generator:latest .
```

To publish the image to Azure Container Registry, run the below command.

```shell
docker login -u kgscr kgscr.azurecr.io
docker image tag url-shortening-key-generator:latest kgscr.azurecr.io/kgscr/url-shortening-key-generator:latest
docker image push kgscr.azurecr.io/kgscr/url-shortening-key-generator:latest
```

To run a container from the image locally, run the below command.

```shell
docker container run -d --name url-shortening-key-generator --publish 80:80 url-shortening-key-generator:latest
```

```shell
kubectl create -f deployment-definition.yaml
kubectl create -f service-definition.yaml
kubectl describe service url-shortening-key-generator-service
```
