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
2. In Module > Presntation > 
	- Create AssemblyReference.cs
3. API > Extensions > ServiceExtensions.cs
    - Add AddModuleInfrastructure from DependencyInjection to AddInfrastructures method    
4. In Module > Application > Interfaces > Repositories
	- Create IModuleRepositoryManager.cs
5. In Module > Infrastructure > DataAccess > Repositories 
    - Implement ModuleRepositoryManager.cs
6. In Module > Application > Interfaces
	- Create IServiceManager.cs	
7. In Module > Application > Mappings
	- Create MappingProfile.cs
8. API > Extensions > ServiceExtensions.cs
    - Add AddModuleInfrastructures from DependencyInjection to AddInfrastructures method    
9. In Module > Application > Services
	- Implement ServiceManager.cs
10. In Module >	Presentation
	- Add AssemblyReference.cs
11. In Module > Domain > Entities
	- Create Entities.cs
12. In Module > Infrastructure > DataAccess > ModuleDBContext.cs
	- Add Entities DbSets
13. In Module > Application > Interfaces > Repositories
	1. Create IEntityRepository.cs
	2. Add IEntityRepository to IModuleRepositoryManager.cs
14. In Module > Infrastructure > DataAccess > Repositories
    1. Implement EntityRepository.cs
    2. Add EntityRepository to ModuleRepositoryManager.cs
15. In Module > Application > DTOs
	- Create EntityDto.cs, EntityForCreationDto.cs, EntityForManipulationDto.cs, EntityForUpdateDto.cs
16. In Module > Application > Mappings
	-  Add mapping to MappingProfile.cs
17. In Module > Application > Interfaces > Services
	1. Create IEntityService.cs
	2. Add IEntityService to IServiceManager.cs
18. In Module > Application > Services
	1. Implement EntityService.cs
	2. Add EntityService to ServiceManager.cs
19. In Module > Presentation > Controllers
	- Create EntitiesController
20. In API > Program.cs
	- Add EntitiesController to 
	