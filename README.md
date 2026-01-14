
# Name
Noavaran Group ERP

## System Architecture
![architectures](readme-files/architectures.webp)
![modular Monolithic](readme-files/MODULAR-Monolith-Architecture.webp)

## Module Architecture
Onion Architecture

![onion architecture ><](readme-files/onion-architecture.webp)

# Naming Conventions
https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/

Every module will be further split into API, Core, and Infrastructure projects to enforce Clean Onion Architecture.
Cross Module communication can happen only via Interfaces

# Libraries

### Serialization/Deserialization
System.Text.Json

### Unit testing
xUnit

### Logging
Serilog

## EF Core tools
### .NET CLI

**install**

`dotnet tool install --global dotnet-ef`

**update**

`dotnet tool update --global dotnet-ef`

Before you can use the tools on a specific project, you'll need to add the *Microsoft.EntityFrameworkCore.Design* and *Microsoft.EntityFrameworkCore.SqlServer* packages to it.

**dotnet ef dbcontext scaffold**

`dotnet ef dbcontext scaffold "Server=.\sql19;Database=NGERP;User Id=sa;Password=123;Trusted_Connection=True;Encrypt=False;" Microsoft.EntityFrameworkCore.SqlServer -d -o Domain\Entities -c ModuleDbContext --context-dir Infrastructure\DataAccess --schema ModuleSchema`

this command should be exexute at root of project

**Adds a new migration**

`dotnet ef migrations add <Name> -o Infrastructure\Migrations -c ModuleDbContext`

**Updates the database to the last migration or to a specified migration**

`dotnet ef database update`

### Package Manager Console

**install**

`Install-Package Microsoft.EntityFrameworkCore.Tools`

**update**

`Update-Package Microsoft.EntityFrameworkCore.Tools`

**Adds a new migration**

`Add-Migration <Name> -context ModuleDbContext`

**Updates the database to the last migration or to a specified migration**

`Update-Database`

## Creating project flow
1. Create four class library projects as follows:
	- NGErp.Module.API
	- NGErp.Module.Domain
	- NGErp.Module.Infrastructure
	- NGErp.Module.Service
2. In Module.Infrastructure > DataAccess
	- Create ServiceCollectionExtensions.cs
3. In Module.API > 
	- Create AssemblyReference.cs
4. NGErp.API > Extensions > ServiceExtensions.cs
    - Add AddModuleInfrastructure from ServiceCollectionExtensions to AddInfrastructures method  
5. In Module.Service >
	- Create ServiceCollectionExtensions.cs
6. NGErp.API > Extensions > ServiceExtensions.cs
    - Add AddModuleServices from ServiceCollectionExtensions to AddServices method    
7. In Module.Service > Mappings
	- Create MappingProfile.cs
8. In Module.Domain > Entities
	- Create Entities.cs
9. In Base.Infrastructure > DataAccess > MainDBContext.cs
	- Add Entities DbSets
10. In Module.Infrastructure > DataAccess > Repositories
	1. Create IEntityRepository.cs
    2. Implement EntityRepository.cs
	3. Add DI to AddModuleInfrastructureServices
11. In Module.Service > DTOs
	- Create EntityDto.cs, CreateEntityDto.cs, UpdateEntityDto.cs
12. In Module.Service > Mappings
	-  Add mapping to MappingProfile.cs
13. In Module.Service > Services
	1. Create IEntityService.cs
	2. Implement EntityService.cs
	3. Add DI To AddEntityServices
14. In Module.API > Controllers
	- Create EntitiesController
15. In NGErp.API > ServiceExtensions
	- Add EntitiesController to ConfigureControllers
	