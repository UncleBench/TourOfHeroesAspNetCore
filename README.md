# Tour of Heroes full-stack application

A little project using the Angular Tour of Heroes tutorial as a starting point.

It uses the following tech stack so far:

## Frontend

- Angular 17
  - Standalone components
  - Consistent use of `inject()` instead of stuffing the constructor

## Backend

- ASP.NET Core 8
- ErrorOr for fluent responses
- FluentValidation (WIP)
- A RESTful heroes endpoint
- An error endpoint along with a custom ProblemDetailsFactory providing ProblemDetails with an Errors extension
- An auth endpoint for authentication, following the RFC 7519 specification (JWT)
- NSwag with a build task to generate Typescript and C# API clients
- MediatR to realize CQRS
- Mapster to map objects between layers
- Entity Framework Core as the ORM
- SQLite as a database

## Credit where credit is due

The backend of this project took heavy inspiration from Amichai Mantinband's YouTube series on [ASP.NET 6 REST API Following CLEAN ARCHITECTURE & DDD Tutorial](https://www.youtube.com/playlist?list=PLzYkqgWkHPKBcDIP5gzLfASkQyTdy0t4k) and his [Clean Architecture template](https://github.com/amantinband/clean-architecture).