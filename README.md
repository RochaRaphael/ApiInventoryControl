# ApiInventoryControl

Código em desenvolvimento!

Essa API tem o foco de entender a utilização do JWT.


## Padrão
Usei o padrão MVC mas sem o View por enqunato, então usei apenas o "M" (Model) e o "C"(Controller).

A pasta `Data` faz parte do uso do Entity Framework, para fazer o mapeamento fluente. As configurações do mapeamento estão dentro da pasta `Mapping`.

A pasta `ViewModels` armazena um modelo de inserção de dados, já que não faz sentido o uso do Model.Login que exige a entrada de todos os dados, pois no mapeamento foi configurado que a propriedade ID será gerada automaticamente pelo banco de dados.

O `ResulViewModel` foi feito para padronizar as respostas da API, assim facilitando o trabalho do Front-end que saberá o que esperar de retorno. 

---------------------------------------------
Tecnologias usadas:
- ASP.NET 6
- Entity Framework
- SQL Server
- Docker
- Azure Data Studio

------------------------------------------------
### Executando a Aplicação Localmente 🔥

- Caso não tenha o .NET instalado, [clique aqui](https://balta.io/blog/dotnet-instalacao-configuracao-e-primeiros-passos) para entender como instalar.

- Para os dados serem inseridos no banco, usaremos o Azure Data Studio, para baixá-lo [clique aqui](https://azure.microsoft.com/pt-br/products/data-studio/#overview)

- Caso não tenha o sistema operacional Linux, baixe o Docker. As instruções para instalação do mesmo [está aqui](https://balta.io/blog/docker-instalacao-configuracao-e-primeiros-passos)

- Instale o Sql no docker. [Clique aqui](https://balta.io/blog/sql-server-docker) para as instruções 

Se tiver feita as instalações conforme os tutoriais, você chegará em uma ela do Docker parecida com essa, onde terá pelo menos o container `sqlserver`
Clique no botão de Play indicado pela seta para rodar o Docker e conseguirmos conectar com o Azure Data Studio

![Doker](https://user-images.githubusercontent.com/109389657/205405654-27f5f268-5e89-4ae8-a870-1c28c9b20c60.PNG)


Agora abra o Azure Data Studio e gere uma nova conexão
Os dados de conexão são os da imagem e a senha é: 1q2w3e4r@#$

![Conexão](https://user-images.githubusercontent.com/109389657/205406558-62e7ca62-338f-4bb9-81c4-650b1a2e7df8.PNG)


Para executar localmente a aplicação você precisa entrar na pasta `ApiLogin` e executar o seguinte comando:

```
> dotnet run
```

Para que, possamos executar o `Entity Framework` no projeto, se faz necessário executar os seguintes comandos dentro da pasta HospitalWarehouse
```
> dotnet tool install --global dotnet-ef
> dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
> dotnet add package Microsoft.EntityFrameworkCore.Design
> dotnet add package Microsoft.EntityFrameworkCore.Sqlite
> dotnet restore
> dotnet ef migrations add InitialCreate
> dotnet ef database update
```
--------------------------------------------
Para fazer requisições a API, use o Postman, [clique aqui](https://www.postman.com/downloads/) para baixar.

Caso o Postman seja novo para você, [clique aqui](https://www.youtube.com/watch?v=op81bMbgZXs&t=252s> para ver esse vídeo do professor Vinicius Dias da Alura ensinando como usar o Postman.

# Tenho Dúvidas... O que Faço?! ❓
Caso tenham dúvidas aos códigos desenvolvidos aqui, sintam-se a vontade em abrir uma [ISSUE AQUI](https://github.com/RochaRaphael/ApiInventoryControl/issues). Assim que possível, estarei respondendo as todas as dúvidas que tiverem!

