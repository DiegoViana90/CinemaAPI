# CinemaApi :camera_flash:

Este é um projeto de API RESTful para gerenciar filmes em salas de cinema, desenvolvido em .NET 7.0 com uma arquitetura em camadas simples MVC. A API permite gerenciar salas (Room) e filmes (Movie) que passam em uma determinada sala.

Implementar uma API resful em dotnet7
Pode ser utilizado uma arquitetura em camadas simples MVC
A API deve ser possível gerenciar os filmes que passam em uma determinando sala de cinema
- Sala (Número da Sala, Descrição)
- Filme (Nome, diretor, duração)
- Uma sala pode ter vários filmes
- um filme pode existir sem uma sala

## Estrutura do Projeto

- **Models**: Contém as classes das entidades `Room`, `Movie` e `MovieRoom`.
- **DTOs**: Contém os objetos de transferência de dados (Data Transfer Objects).
- **Repositories**: Contém a lógica de acesso ao banco de dados.
- **Services**: Contém a lógica de negócios.
- **Controllers**: Contém os endpoints da API.
- **Tests**: Contém os testes unitários para controllers e serviços.

## Funcionalidades

- Gerenciamento de Salas (Room)
- Gerenciamento de Filmes (Movie)
- Suporte a testes unitários
- Utilização do banco MySQL
- Configuração do Swagger para documentação da API
- Suporte a Docker e Docker Compose

## Requisitos

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [MySQL](https://www.mysql.com/downloads/)
- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)

## Configuração e Execução

### Passo 1: Clonar o Repositório

```bash
git clone https://github.com/DiegoViana90/CinemaAPI.git
cd CinemaApi
```
### Passo 2: Configurar o Banco de Dados
Certifique-se de que o MySQL esteja instalado e em execução.
Atualize a string de conexão no arquivo appsettings.json com suas credenciais do MySQL:
```bash
  "ConnectionStrings": {
    "DefaultConnection": "Server=127.0.0.1;Database=CinemaDb;User=root;Password=yourpassword;"
  },
```

### Passo 3: Restaurar Dependências
```bash
dotnet restore
```
### Passo 4: Executar as Migrações do Entity Framework
```bash
dotnet ef database update
```
### Passo 5: Execução do Projeto
Você pode escolher entre executar o projeto localmente ou usando Docker:

- Opção A: Executar Localmente
```bash
dotnet run
```

Opção B: Executar com Docker
- Para construir e executar os contêineres Docker, use:
```bash
docker-compose up --build
```
Nota: Certifique-se de que o arquivo docker-compose.yml esteja configurado corretamente para incluir a configuração do banco de dados e da aplicação.

###  Passo 6: Acessar a Documentação da API
A documentação da API pode ser acessada em:
- Com 'dotnet run' : http://localhost:5005/swagger/index.html
- Com 'docker-compose up --build': http://localhost:8080/swagger/index.html

### Passo 7: Executar Testes Unitários
-Os testes unitários para controllers e serviços podem ser encontrados na pasta `Tests`. 
Para executar os testes, use:
```bash
dotnet test
```
