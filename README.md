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


## Creating project flow
1. In Module > Infrastructure > DataAccess
	1. Create ModuleDBContext.cs
	2. Create DependencyInjection.cs
	3. Create ModuleDBContextFactory.cs
2. API > Extensions > ServiceExtensions.cs
    - Add DependencyInjection to AddDbContexts method    
3. In Module > Application > Interfaces > Repositories
	- Create IModuleRepositoryManager.cs
4. In Module > Infrastructure > DataAccess > Repositories 
    - Implement ModuleRepositoryManager.cs
5. In Module > Application > Interfaces
	- Create IServiceManager.cs
6. In Module > Application > Services
	- Implement ServiceManager.cs
7. In Module > Domain > Entities
	- Create Entities.cs
8. In Module > Infrastructure > DataAccess > ModuleDBContext.cs
	- Add Entities DbSets
9. In Module > Application > Interfaces > Repositories
	1. Create IEntityRepository.cs
	2. Add IEntityRepository to IModuleRepositoryManager.cs
10. In Module > Infrastructure > DataAccess > Repositories
    1. Implement EntityRepository.cs
    2. Add EntityRepository to ModuleRepositoryManager.cs
11. In Module > Application > DTOs
	- Create EntityDto.cs, EntityForCreationDto.cs, EntityForManipulationDto.cs, EntityForUpdateDto.cs
12. In API > MappingProfile.cs
	- Add Map between Entity and Dtos	
13. In Module > Application > Interfaces > Services
	1. Create IEntityService.cs
	2. Add IEntityService to IServiceManager.cs
14. In Module > Application > Services
	1. Implement EntityService.cs
	2. Add EntityService to ServiceManager.cs
    