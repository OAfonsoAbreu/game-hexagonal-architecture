# game-hexagonal-architecture
Projeto para implementar arquitetura hexagonal com DDD

Projeto implementa JWT para autenticação e autorização.
Para conseguir autenticar, utilize o usuário: admin e senha: admin no endpoint .../v1/auth/authenticate

Com o token de retorno, utilize no header das requisições por meio da tag Authorization : bearer token_gerado

Para facilitar os testes foi implementado Swagger
Caso opte por testar via Postman, importe o .json gerado pelo Swagger em .../swagger/v1/swagger.json

Como banco de dados foi utilizado LiteDB (https://www.litedb.org/)

Para carga inicial de games e usuários acesse respectivamente os endpoints: .../playstation/games e .../user/users

Projeto conta com Dockerfile e Docker-compose.
