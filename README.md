# Shenam API

[![.NET](https://img.shields.io/badge/.NET-6.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Build](https://img.shields.io/badge/build-passing-brightgreen)](#)
[![Tests](https://img.shields.io/badge/tests-unit-informational)](docs/testing.md)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

A clean, production-ready REST API for rental housing workflows, built with .NET 6.

## 🌐 Language
- 🇬🇧 **English** (current)
- 🇺🇿 **Uzbek** → [README.uz.md](README.uz.md)

## Table of Contents
- [Overview](#overview)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Project Architecture](#project-architecture)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [Quick Start](#quick-start)
- [Documentation](#documentation)
- [Contributing](#contributing)
- [License](#license)

## Overview
Shenam API manages rental domain entities:
- Guest
- HostEntity
- Home
- HomeRequest

It provides RESTful CRUD operations with service-layer validation and database persistence.

## Features
- CRUD endpoints for all core entities
- Service-level validation and exception handling
- Entity Framework Core persistence with SQL Server
- Swagger/OpenAPI support
- Unit testing support with mocks and fluent assertions

## Tech Stack
- .NET 6
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger
- RESTFulSense
- xUnit
- Moq
- FluentAssertions

## Project Architecture
The API follows a layered design:
- **Controllers**: HTTP boundary and status mapping
- **Services**: business logic and validation
- **Brokers**: storage and logging abstraction
- **Database**: SQL Server persistence

For detailed architecture and diagrams, see [docs/architecture.md](docs/architecture.md).

## Project Structure
```text
Shenam/
├── README.md
├── README.uz.md
├── LICENSE
└── docs/
    ├── architecture.md
    ├── api.md
    ├── diagrams.md
    ├── testing.md
    ├── deployment.md
    └── troubleshooting.md
```

## Getting Started
1. Clone repository
2. Configure connection string
3. Apply EF migrations
4. Run API
5. Open Swagger

## Quick Start
```bash
git clone <REPO_URL>
cd Shenam/Shenam.API
dotnet restore Shenam.API.sln
dotnet build Shenam.API.sln
dotnet ef database update --project Shenam.API/Shenam.API.csproj
cd Shenam.API
dotnet run
```

Swagger: `https://localhost:<port>/swagger`

## Documentation
- Full architecture → [docs/architecture.md](docs/architecture.md)
- Full API documentation → [docs/api.md](docs/api.md)
- Diagrams → [docs/diagrams.md](docs/diagrams.md)
- Testing guide → [docs/testing.md](docs/testing.md)
- Deployment guide → [docs/deployment.md](docs/deployment.md)
- Troubleshooting → [docs/troubleshooting.md](docs/troubleshooting.md)

## Contributing
Please use feature branches and small, focused PRs.
1. Fork repository
2. Create branch (`feature/<name>`)
3. Add/adjust tests when needed
4. Update docs for behavior changes
5. Open a pull request

## License
Licensed under the [MIT License](LICENSE).
