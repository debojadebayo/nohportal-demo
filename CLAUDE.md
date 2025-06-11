# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) or any alternative coding agent when working with code in this repository.

## Build Commands
- Run Dockerized: `docker-compose up`
- Build Client: `dotnet build Client/Client.csproj`
- Build Server: `dotnet build Server/WebApi/WebApi.csproj`
- Run Migrations: `cd Server && ./run-migrations.sh`
- Debug Server: `cd Server/WebApi && dotnet watch run`

## Code Style Guidelines
- Use C# 9.0+ features with nullable reference types
- Follow standard C# naming conventions (PascalCase for types, camelCase for fields)
- Validate DTOs with FluentValidation (see Shared/Validators)
- Use strongly-typed generic patterns for base classes
- Structure exceptions with try/catch blocks that log errors and return appropriate IResult
- Use async/await for asynchronous operations
- Group related functionality into modules (CRM, Scheduling)
- Implement interfaces and follow dependency injection patterns
- Use EF Core for database operations
- Document public APIs with XML comments

## Terraform 
- WHen making any changes to terraform code. Ensure to run terraform fmt -recursive first to ensure that the code is formatted correctly
