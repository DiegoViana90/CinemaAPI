# CinemaApi

Este é um projeto de API RESTful para gerenciar filmes em salas de cinema, desenvolvido em .NET 7.0 com uma arquitetura em camadas simples MVC. A API permite gerenciar salas (Room) e filmes (Movie) que passam em uma determinada sala.

## Estrutura do Projeto

- **Entities**: Contém as classes das entidades `Room` e `Movie`.
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
    "DefaultConnection": "Server=localhost;Database=CinemaDb;User=root;Password=yourpassword;"
  },
```

### Passo 3: Restaurar Dependências
```bash
dotnet restore
```
