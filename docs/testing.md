# Testing Guide

## Testing Strategy
Shenam API focuses on unit testing the service layer where business rules and validation reside.

## What to Test
- Input validation
- Business logic rules
- Exception mapping and propagation
- CRUD behavior through mocked brokers

## Testing Stack
- **xUnit** for test framework
- **Moq** for mocking dependencies
- **FluentAssertions** for expressive assertions

## Running Tests
From solution root:
```bash
cd Shenam.API
dotnet test Shenam.API.sln
```

Run only unit-test project:
```bash
dotnet test Shenam.Api.Tests.Unit/Shenam.Api.Tests.Unit.csproj
```

## Suggested Test Naming
Use behavior-driven naming:
- `ShouldAddGuestAsyncWhenGuestIsValid`
- `ShouldThrowValidationExceptionOnAddIfGuestIsNull`

## Good Practices
- Keep tests deterministic
- Arrange test data explicitly
- Assert both returned data and interactions
- Test positive and negative paths
