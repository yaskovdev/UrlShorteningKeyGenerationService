# Url Shortening Key Generation Service

In order to build docker image, run the below command from the same directory as this README.md.

```shell
docker image build -f UrlShorteningKeyGenerationService/Dockerfile -t url-sorting-key-generation-service:latest .
```

```shell
docker container run -d --name url-sorting-key-generation-service --publish 80:80 url-sorting-key-generation-service:latest
```
