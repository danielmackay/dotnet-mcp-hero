# .NET Hero API with Model Context Protocol

A .NET-based superhero management system comprised of a Clean Architecture API backend and a Model Context Protocol (MCP) server for AI assistant integration.

![.NET 9.0](https://img.shields.io/badge/.NET-9.0-blue)
![Architecture](https://img.shields.io/badge/Architecture-Clean-green)
![MCP](https://img.shields.io/badge/AI-MCP-purple)

## 🎯 Project Overview

This project demonstrates how to build a modern .NET application with clean architecture principles and integrate it with the Model Context Protocol (MCP) for AI assistant capabilities. It consists of two main parts:

1. **HeroApi** - A .NET 9 WebAPI built with Clean Architecture principles
2. **HeroMcp** - A Model Context Protocol server that provides AI tool capabilities for interacting with the API

### Domain

The application is built around a superhero domain model that includes:

- **Heroes** - Characters with powers, name, alias, and power level
- **Teams** - Groups of heroes that can undertake missions
- **Missions** - Tasks that teams can execute and complete

## 🚀 Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/products/docker-desktop/) (for database)
- [VS Code](https://code.visualstudio.com/) or other .NET IDE

### Running the Hero API

1. Navigate to the AppHost directory:

```bash
cd Api/tools/AppHost
```

2. Run the application with the .NET CLI:

```bash
dotnet run
```

This will:
- Start a SQL Server container for the database
- Run migrations to set up the database schema
- Start the HeroApi on https://localhost:7255

3. Open https://localhost:7255/scalar/v1 in your browser to view the API documentation

### Running the MCP Server

1. Navigate to the Mcp directory:

```bash
cd Mcp
```

2. Run the MCP server:

```bash
dotnet run
```

This starts the MCP server which provides tools for interacting with the HeroApi.

### Generating the API Client

The project uses Microsoft Kiota to generate a strongly-typed API client. To regenerate the client:

1. Ensure the HeroApi is running
2. Navigate to the Mcp directory:

```bash
cd Mcp
```

3. Run the Kiota generator:

```bash
dotnet kiota generate --openapi https://localhost:7255/openapi/v1.json --language csharp --class-name HeroApi --clean-output --additional-data false
```

This will regenerate the API client in the `output` directory based on the latest OpenAPI specification.

## ✨ Features

### HeroApi

- **Clean Architecture**: Separation of concerns with Domain, Application, Infrastructure, and WebApi layers
- **Domain-Driven Design**: Rich domain model with aggregates, entities, and value objects
- **CQRS Pattern**: Separation of commands and queries using MediatR
- **Minimal APIs**: Fast and lightweight API endpoints
- **OpenAPI/Scalar**: Modern, interactive API documentation
- **EF Core**: Data access with Entity Framework Core
- **Aspire Dashboard**: For observability and resource orchestration
- **Strongly Typed IDs**: Using Vogen to prevent primitive obsession
- **Health Checks**: Monitor application health
- **Comprehensive Testing**: Architecture tests, domain unit tests, and API integration tests

### HeroMcp

- **Model Context Protocol**: Integration with AI assistants using the MCP standard
- **AI Tools**: Custom tools for managing heroes, teams, and missions
- **Generated API Client**: Uses Microsoft Kiota to access the HeroApi

## 📋 API Endpoints

The HeroApi provides the following endpoints:

- **GET /api/heroes** - Get all heroes
- **POST /api/heroes** - Create a new hero
- **GET /api/teams** - Get all teams
- **GET /api/teams/{id}** - Get a specific team
- **POST /api/teams** - Create a new team
- **POST /api/teams/{id}/heroes/{heroId}** - Add a hero to a team
- **POST /api/teams/{id}/execute-mission** - Execute a mission with a team
- **POST /api/teams/{id}/complete-mission** - Complete a team's current mission

## 🤖 MCP Tools

The MCP server provides the following tools to AI assistants:

- **GetHeroes** - Retrieve all heroes from the API
- **CreateHero** - Create a new hero
- **GetTeams** - Retrieve all teams from the API
- **GetTeam** - Get a specific team by ID
- **CreateTeam** - Create a new team
- **AddHeroToTeam** - Add a hero to a team
- **ExecuteMission** - Execute a mission with a team
- **CompleteMission** - Complete a team's mission
- **Echo** and **ReverseEcho** - Simple tools for testing the MCP connection

## 🏗️ Project Structure

```
dotnet-mcp-hero/
├── Api/                           # Clean Architecture API
│   ├── src/                       # Source code
│   │   ├── Application/           # Application layer (use cases)
│   │   ├── Domain/                # Domain layer (business entities)
│   │   ├── Infrastructure/        # Infrastructure layer
│   │   └── WebApi/                # WebApi layer (controllers)
│   ├── tests/                     # Test projects
│   │   ├── Architecture.Tests/    # Architecture tests
│   │   ├── Domain.UnitTests/      # Domain unit tests
│   │   └── WebApi.IntegrationTests/  # API integration tests
│   └── tools/                     # Developer tools
│       ├── AppHost/               # Aspire host
│       └── MigrationService/      # Database migrations
├── Mcp/                           # Model Context Protocol server
│   ├── Program.cs                 # MCP server setup
│   ├── Tools/                     # MCP tools
│   │   ├── Echo/                  # Echo tools
│   │   ├── Heroes/                # Hero management tools
│   │   └── Teams/                 # Team management tools
│   └── output/                    # Generated API client
└── McpHero.sln                    # Solution file
```

## 📚 Architecture

This project follows Clean Architecture principles with the following layers:

1. **Domain Layer** - Contains business entities, aggregates, value objects, and domain events
2. **Application Layer** - Contains business logic, commands, queries, and interfaces
3. **Infrastructure Layer** - Implements interfaces from the application layer
4. **WebApi Layer** - Exposes the API endpoints

## 🧪 Testing

- **Architecture Tests** - Verifies that the codebase adheres to clean architecture principles
- **Domain Unit Tests** - Tests the business logic in isolation
- **Integration Tests** - Tests the API endpoints against a real database

## 📖 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🙏 Acknowledgements

- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) - Robert C. Martin
- [Architectural Decision Records](https://adr.github.io/) - For documenting architectural decisions
- [Model Context Protocol](https://github.com/microsoft/modelcontextprotocol) - Microsoft's protocol for AI tool integration