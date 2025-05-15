# Name
Noavaran Group ERP

## Architecture
Modular Monolith

## Naming Conventions
https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/

Every module will be further split into API, Core, and Infrastructure projects to enforce Clean Onion Architecture.
Cross Module communication can happen only via Interfaces

## Libraries

### Serialization/Deserialization
System.Text.Json

### Unit testing
xUnit

### Logging
Serilog
