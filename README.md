# fiap.tech-challenge.fast-food.api.payments

### CLI
Para iniciar a aplicação e banco de dados separadamente, siga os passos abaixo.

Para subir o BD local, o recomendado é utilizar o Docker e executar o seguinte comando:
```shell
docker run --rm --name pg-payments-docker -e POSTGRES_PASSWORD=docker -e POSTGRES_DB=payments_db -d -p 5433:5432 postgres
```

Obs.: A *connection string* já está configurada corretamente no arquivo *launchSettings.json*

Inicie a Aplicação (API):
```shell
dotnet run --project .\src\Drivers\API\API.csproj
```

Caso seja necessário derrubar o BD, basta executar:

```shell
docker container kill pg-payments-docker
```