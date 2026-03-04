# Deployment Guide

## Overview
This document describes how to prepare and deploy Shenam API to production-like environments.

## Environment Configuration
Use environment-specific settings for:
- `ASPNETCORE_ENVIRONMENT`
- `ConnectionStrings:DefaultConnection`
- logging levels

Do not store secrets in source control. Use:
- environment variables
- secret manager
- cloud key vault solutions

## Build and Publish
```bash
cd Shenam.API
dotnet restore Shenam.API.sln
dotnet build Shenam.API.sln -c Release
dotnet publish Shenam.API/Shenam.API.csproj -c Release -o ./publish
```

## Database Migration
Apply migrations in target environment before serving traffic:
```bash
dotnet ef database update --project Shenam.API/Shenam.API.csproj
```

## Deployment Targets
- Azure App Service
- Docker container runtime
- VM with IIS/Kestrel + reverse proxy

## CI/CD Explanation
The repository includes an infrastructure build project (`Shenam.Api.Infrastructure.Build`) that generates GitHub workflow definitions.

Typical pipeline:
1. Checkout source
2. Setup .NET SDK
3. Restore dependencies
4. Build solution
5. Run tests
6. Publish artifacts
7. Deploy to environment

## Post-Deployment Checklist
- Verify health and startup logs
- Verify DB connectivity
- Verify Swagger endpoint access (if enabled)
- Run smoke tests on critical endpoints
- Monitor error rates and latency
