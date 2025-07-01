#!/bin/bash

# Exit immediately if a command exits with a non-zero status.
set -e

# --- Configuration ---
MODULES_BASE_DIR="Modules"
PROJECT_NAMESPACE_PREFIX="Server.Modules" # Used for C# namespaces
DEFAULT_TARGET_FRAMEWORK="net9.0" # Adjust if your project uses a different .NET version
DEFAULT_EF_CORE_VERSION="9.0.0" # Adjust to your project's EF Core version

# --- Prompt for Module Name ---
echo "Enter the name for the new module (e.g., Sales, Inventory):"
read -r MODULE_NAME_INPUT

if [ -z "$MODULE_NAME_INPUT" ]; then
  exit 1
fi

# --- Derive Naming Conventions ---
# PascalCase: e.g., Sales, InventoryManagement
MODULE_NAME_PASCAL=$(echo "$MODULE_NAME_INPUT" | awk '{for(i=1;i<=NF;i++) $i=toupper(substr($i,1,1)) substr($i,2); print}' | sed 's/[^a-zA-Z0-9]//g')

# lowercase: e.g., sales, inventorymanagement
MODULE_NAME_LOWER=$(echo "$MODULE_NAME_PASCAL" | tr '[:upper:]' '[:lower:]')

# Define project names
MAIN_PROJECT_NAME="Server.Modules.${MODULE_NAME_PASCAL}"
APPLICATION_PROJECT_NAME="Server.Modules.${MODULE_NAME_PASCAL}.Application"
ENDPOINTS_PROJECT_NAME="Server.Modules.${MODULE_NAME_PASCAL}.Endpoints"
ENTITIES_PROJECT_NAME="Server.Modules.${MODULE_NAME_PASCAL}.Entities"
INFRA_PROJECT_NAME="Server.Modules.${MODULE_NAME_PASCAL}.Infrastructure"
SERVICES_PROJECT_NAME="Server.Modules.${MODULE_NAME_PASCAL}.Services"

# Define assembly and root namespaces
MAIN_ASSEMBLY_NAME="${PROJECT_NAMESPACE_PREFIX}.${MODULE_NAME_PASCAL}"
INFRA_ASSEMBLY_NAME="${PROJECT_NAMESPACE_PREFIX}.${MODULE_NAME_PASCAL}.Infrastructure"
MAIN_ROOT_NAMESPACE="${PROJECT_NAMESPACE_PREFIX}.${MODULE_NAME_PASCAL}"
INFRA_ROOT_NAMESPACE="${PROJECT_NAMESPACE_PREFIX}.${MODULE_NAME_PASCAL}.Infrastructure"


echo "--------------------------------------------------"
echo "Generating module with the following names:"
echo "PascalCase: $MODULE_NAME_PASCAL"
echo "lowercase:  $MODULE_NAME_LOWER"
echo "--------------------------------------------------"
echo "Target Framework: $DEFAULT_TARGET_FRAMEWORK"
echo "EF Core Version: $DEFAULT_EF_CORE_VERSION"
echo "--------------------------------------------------"
echo "Press Enter to continue or Ctrl+C to abort."
read -r

# --- Define Paths ---
MODULE_DIR="$MODULES_BASE_DIR/$MODULE_NAME_PASCAL"
MODULE_ENTITIES_DIR="$MODULE_DIR/Entities"
MODULE_APPLICATION_DIR="$MODULE_DIR/Application"
MODULE_INFRASTRUCTURE_DIR="$MODULE_DIR/Infrastructure"
MODULE_DATABASE_DIR="$MODULE_INFRASTRUCTURE_DIR/Database"
MODULE_MAPPERS_DIR="$MODULE_INFRASTRUCTURE_DIR/Mappers"
MODULE_MIGRATIONS_DIR="$MODULE_DATABASE_DIR/Migrations" # EF Core will create this if it doesn't exist, but good to have
MODULE_CONFIGURATIONS_DIR="$MODULE_DATABASE_DIR/Configurations"
MODULE_ENDPOINTS_DIR="$MODULE_DIR/Endpoints"
MODULE_SERVICES_DIR="$MODULE_DIR/Services"

# Define .csproj paths
MAIN_CSPROJ_FILE_PATH="$MODULE_DIR/${MAIN_PROJECT_NAME}.csproj"
APPLICATION_CSPROJ_FILE_PATH="$MODULE_APPLICATION_DIR/${APPLICATION_PROJECT_NAME}.csproj"
ENDPOINTS_CSPROJ_FILE_PATH="$MODULE_ENDPOINTS_DIR/${ENDPOINTS_PROJECT_NAME}.csproj"
ENTITIES_CSPROJ_FILE_PATH="$MODULE_ENTITIES_DIR/${ENTITIES_PROJECT_NAME}.csproj"
INFRA_CSPROJ_FILE_PATH="$MODULE_INFRASTRUCTURE_DIR/${INFRA_PROJECT_NAME}.csproj"
SERVICES_CSPROJ_FILE_PATH="$MODULE_SERVICES_DIR/${SERVICES_PROJECT_NAME}.csproj"

# --- Create Directory Structure ---
echo "Creating directory structure for module $MODULE_NAME_PASCAL in $MODULE_DIR..."
mkdir -p "$MODULE_ENTITIES_DIR"
mkdir -p "$MODULE_APPLICATION_DIR"
mkdir -p "$MODULE_MIGRATIONS_DIR"
mkdir -p "$MODULE_MAPPERS_DIR"
mkdir -p "$MODULE_CONFIGURATIONS_DIR"
mkdir -p "$MODULE_ENDPOINTS_DIR"
mkdir -p "$MODULE_SERVICES_DIR"

echo "Directory structure created."

# --- Generate Files ---

# --- Domain: Entity ---
ENTITY_FILE_PATH="$MODULE_ENTITIES_DIR/ExampleEntity.cs"
echo "Generating $ENTITY_FILE_PATH..."
cat << EOF > "$ENTITY_FILE_PATH"
// filepath: $ENTITY_FILE_PATH
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Shared.DTOs;

namespace ${MAIN_ROOT_NAMESPACE}.Entities;

public class ExampleEntity : BaseEntity<ExampleEntity>, IEntity, IAuditEntity
{
    public string? Name { get; set; }
}
public class ExampleEntityDto : IDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}
EOF

# --- Infrastructure: Schema Constants ---
SCHEMA_FILE_PATH="$MODULE_DATABASE_DIR/Schema.cs"
echo "Generating $SCHEMA_FILE_PATH..."
cat << EOF > "$SCHEMA_FILE_PATH"
// filepath: $SCHEMA_FILE_PATH
namespace ${INFRA_ROOT_NAMESPACE}.Database;

public static class ${MODULE_NAME_PASCAL}Schema
{
    public const string Name = "${MODULE_NAME_LOWER}";
}
EOF

# --- Infrastructure: DbContext ---
DBCONTEXT_FILE_PATH="$MODULE_DATABASE_DIR/${MODULE_NAME_PASCAL}DbContext.cs"
echo "Generating $DBCONTEXT_FILE_PATH..."
cat << EOF > "$DBCONTEXT_FILE_PATH"
// filepath: $DBCONTEXT_FILE_PATH
using Microsoft.EntityFrameworkCore;
using ${MAIN_ROOT_NAMESPACE}.Entities;
using ComposedHealthBase.Server.Database;

namespace ${INFRA_ROOT_NAMESPACE}.Database;

public sealed class ${MODULE_NAME_PASCAL}DbContext(DbContextOptions<${MODULE_NAME_PASCAL}DbContext> options) : BaseDbContext<${MODULE_NAME_PASCAL}DbContext>(options), IDbContext<${MODULE_NAME_PASCAL}DbContext>
{
    public DbSet<ExampleEntity> ExampleEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(${MODULE_NAME_PASCAL}Schema.Name);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(${MODULE_NAME_PASCAL}DbContext).Assembly);
    }
}
EOF

# --- Infrastructure: DesignTimeDbContextFactory ---
DESIGNTIMEDBCONTEXTFACTORY_FILE_PATH="$MODULE_DATABASE_DIR/${MODULE_NAME_PASCAL}DesignTimeDbContextFactory.cs"
echo "Generating $DESIGNTIMEDBCONTEXTFACTORY_FILE_PATH..."
cat << EOF > "$DESIGNTIMEDBCONTEXTFACTORY_FILE_PATH"
// filepath: $DESIGNTIMEDBCONTEXTFACTORY_FILE_PATH
using ComposedHealthBase.Server.Database;
using Microsoft.EntityFrameworkCore;

namespace ${INFRA_ROOT_NAMESPACE}.Database;

public class ${MODULE_NAME_PASCAL}DesignTimeDbContextFactory : DesignTimeDbContextFactory<${MODULE_NAME_PASCAL}DbContext>
{
}
EOF

# --- Infrastructure: Mapper ---
MAPPER_FILE_PATH="$MODULE_MAPPERS_DIR/ExampleEntityMapper.cs"
echo "Generating $MAPPER_FILE_PATH..."
cat << EOF > "$MAPPER_FILE_PATH"
using ComposedHealthBase.Server.Mappers;
using Server.Modules.${MODULE_NAME_PASCAL}.Entities;

public class ExampleEntityMapper : IMapper<ExampleEntity, ExampleEntityDto>
{
    public ExampleEntityDto Map(ExampleEntity entity)
    {
        return new ExampleEntityDto
        {

        };
    }

    public ExampleEntity MapWithKeycloakId(ExampleEntityDto dto, Guid keycloakId)
    {
        return new ExampleEntity
        {

        };
    }

    public ExampleEntity Map(ExampleEntityDto dto)
    {
        throw new NotImplementedException("Map method without KeycloakId is not implemented. Use MapWithKeycloakId instead.");
    }

    public IEnumerable<ExampleEntityDto> Map(IEnumerable<ExampleEntity> entities)
    {
        return entities.Select(Map);
    }

    public IEnumerable<ExampleEntity> Map(IEnumerable<ExampleEntityDto> dtos)
    {
        return dtos.Select(Map);
    }

    public void Map(ExampleEntityDto dto, ExampleEntity entity)
    {

    }

    public void Map(ExampleEntity entity, ExampleEntityDto dto)
    {

    }

    public void Map(IEnumerable<ExampleEntityDto> dtos, IEnumerable<ExampleEntity> entities)
    {
        var dtosArray = dtos.ToArray();
        var entitiesArray = entities.ToArray();
        for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
        {
            Map(dtosArray[i], entitiesArray[i]);
        }
    }

    public void Map(IEnumerable<ExampleEntity> entities, IEnumerable<ExampleEntityDto> dtos)
    {
        var dtosArray = dtos.ToArray();
        var entitiesArray = entities.ToArray();
        for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
        {
            Map(entitiesArray[i], dtosArray[i]);
        }
    }
}
EOF

# --- Presentation: API Endpoints ---
ENDPOINTS_FILE_PATH="$MODULE_ENDPOINTS_DIR/${MODULE_NAME_PASCAL}Endpoints.cs"
echo "Generating $ENDPOINTS_FILE_PATH..."
cat << EOF > "$ENDPOINTS_FILE_PATH"
// filepath: $ENDPOINTS_FILE_PATH
using ComposedHealthBase.Server.Endpoints;
using Server.Modules.${MODULE_NAME_PASCAL}.Entities;
using Server.Modules.${MODULE_NAME_PASCAL}.Infrastructure.Database;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using ComposedHealthBase.Server.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using ComposedHealthBase.Server.Queries;
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Database;

using ComposedHealthBase.Shared.DTOs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Server.Modules.${MODULE_NAME_PASCAL}.Endpoints
{
	//public class ManagerEndpoints : BaseEndpoints<Entity, EntityDto, ${MODULE_NAME_PASCAL}DbContext>, IEndpoints { }
}
EOF

# --- Module Definition/Initializer (Module.cs in Main Module Project) ---
MODULE_CS_FILE_PATH="$MODULE_INFRASTRUCTURE_DIR/${MODULE_NAME_PASCAL}Module.cs"
echo "Generating $MODULE_CS_FILE_PATH..."
cat << EOF > "$MODULE_CS_FILE_PATH"
// filepath: $MODULE_CS_FILE_PATH
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Server.Modules.${MODULE_NAME_PASCAL}.Infrastructure.Database;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Modules;

namespace Server.Modules.${MODULE_NAME_PASCAL}.Infrastructure
{
	public class ${MODULE_NAME_PASCAL}Module : IModule
	{
		public IServiceCollection RegisterModuleServices(IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
			services.AddDbContext<IDbContext<${MODULE_NAME_PASCAL}DbContext>, ${MODULE_NAME_PASCAL}DbContext>(options =>
							options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

			return services;
		}
		public WebApplication ConfigureModuleServices(WebApplication app, bool isDevelopment)
		{
			if (isDevelopment)
			{
				using (var scope = app.Services.CreateScope())
				{
					var dbContext = scope.ServiceProvider.GetRequiredService<${MODULE_NAME_PASCAL}DbContext>();
					dbContext.Database.Migrate();
				}
			}
			return app;
		}
	}
}
EOF

# --- Application Project File (.csproj) ---
echo "Generating $APPLICATION_CSPROJ_FILE_PATH..."
cat << EOF > "$APPLICATION_CSPROJ_FILE_PATH"
<!-- filepath: $APPLICATION_CSPROJ_FILE_PATH -->
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>${DEFAULT_TARGET_FRAMEWORK}</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\..\..\..\Shared\Shared.csproj" />
    <ProjectReference Include="..\..\..\ComposedHealthBase\Server\ComposedHealthBase.Server.csproj" />
    <ProjectReference Include="..\Entities\Server.Modules.${MODULE_NAME_PASCAL}.Entities.csproj" />
  </ItemGroup>

</Project>
EOF
# --- Endpoints Project File (.csproj) ---
echo "Generating $ENDPOINTS_CSPROJ_FILE_PATH..."
cat << EOF > "$ENDPOINTS_CSPROJ_FILE_PATH"
<!-- filepath: $ENDPOINTS_CSPROJ_FILE_PATH -->
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>${DEFAULT_TARGET_FRAMEWORK}</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\..\..\..\Shared\Shared.csproj" />
    <ProjectReference Include="..\..\..\ComposedHealthBase\Server\ComposedHealthBase.Server.csproj" />
    <ProjectReference Include="..\Entities\Server.Modules.${MODULE_NAME_PASCAL}.Entities.csproj" />
    <ProjectReference Include="..\Infrastructure\Server.Modules.${MODULE_NAME_PASCAL}.Infrastructure.csproj" />
  </ItemGroup>

</Project>
EOF
# --- Entities Project File (.csproj) ---
echo "Generating $ENTITIES_CSPROJ_FILE_PATH..."
cat << EOF > "$ENTITIES_CSPROJ_FILE_PATH"
<!-- filepath: $ENTITIES_CSPROJ_FILE_PATH -->
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>${DEFAULT_TARGET_FRAMEWORK}</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\..\..\ComposedHealthBase\Server\ComposedHealthBase.Server.csproj" />
  </ItemGroup>

</Project>
EOF
# --- Infrastructure Project File (.csproj) ---
echo "Generating $INFRA_CSPROJ_FILE_PATH..."
cat << EOF > "$INFRA_CSPROJ_FILE_PATH"
<!-- filepath: $INFRA_CSPROJ_FILE_PATH -->
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>${DEFAULT_TARGET_FRAMEWORK}</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\..\..\..\Shared\Shared.csproj" />
    <ProjectReference Include="..\..\..\ComposedHealthBase\Server\ComposedHealthBase.Server.csproj" />
    <ProjectReference Include="..\Entities\Server.Modules.${MODULE_NAME_PASCAL}.Entities.csproj" />
  </ItemGroup>

</Project>
EOF
# --- Services Project File (.csproj) ---
echo "Generating $SERVICES_CSPROJ_FILE_PATH..."
cat << EOF > "$SERVICES_CSPROJ_FILE_PATH"
<!-- filepath: $SERVICES_CSPROJ_FILE_PATH -->
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>${DEFAULT_TARGET_FRAMEWORK}</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>
EOF

echo "--------------------------------------------------"
echo "Module $MODULE_NAME_PASCAL generated successfully with main project $MAIN_PROJECT_NAME and infrastructure project $INFRA_PROJECT_NAME in $MODULE_DIR"
echo "--------------------------------------------------"
echo "Next steps:"
echo "1. Add the projects to your solution file (.sln):"
echo "   cd ../..  # Assuming you are in Server/ and .sln is two levels up from Server/"
echo "   dotnet sln add Server/$MAIN_CSPROJ_FILE_PATH"
echo "   dotnet sln add Server/$INFRA_CSPROJ_FILE_PATH"
echo "   cd Server/ # Return to Server directory"
echo ""
echo "2. Register the module in your main application's Program.cs (or Startup.cs):"
echo "   In your service configuration (e.g., before builder.Build()):"
echo "     builder.Services.Add${MODULE_NAME_PASCAL}Module(builder.Configuration);"
echo "   In your application pipeline configuration (e.g., before app.Run()):"
echo "     app.Use${MODULE_NAME_PASCAL}Module();"
echo ""
echo "3. Configure the connection string for the ${MODULE_NAME_PASCAL} module in appsettings.json:"
echo "   Add a connection string named '${MODULE_NAME_PASCAL}Connection' or ensure 'DefaultConnection' is appropriate."
echo "   Example for '${MODULE_NAME_PASCAL}Connection':"
echo "   \"${MODULE_NAME_PASCAL}Connection\": \"Server=(localdb)\\\\mssqllocaldb;Database=NationOH_${MODULE_NAME_PASCAL};Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False\""
echo ""
echo "4. Create initial migration for the module:"
echo "   Make sure your main API/Server project (the startup project) references the main module project ($MAIN_PROJECT_NAME)."
echo "   The main module project, in turn, references the infrastructure project."
echo "   Run from solution root (e.g., NationOH/):"
echo "   dotnet ef migrations add InitialCreateFor${MODULE_NAME_PASCAL} -p Server/$INFRA_CSPROJ_FILE_PATH -s Server/<PathToYourMainApiProject.csproj> -c ${MODULE_NAME_PASCAL}DbContext"
echo "   Example: dotnet ef migrations add InitialCreateFor${MODULE_NAME_PASCAL} -p Server/$INFRA_CSPROJ_FILE_PATH -s Server/NationOH.Api/NationOH.Api.csproj -c ${MODULE_NAME_PASCAL}DbContext"
echo ""
echo "5. Apply migrations to the database:"
echo "   Run from solution root (e.g., NationOH/):"
echo "   dotnet ef database update -p Server/$INFRA_CSPROJ_FILE_PATH -s Server/<PathToYourMainApiProject.csproj> -c ${MODULE_NAME_PASCAL}DbContext"
echo "   Example: dotnet ef database update -p Server/$INFRA_CSPROJ_FILE_PATH -s Server/NationOH.Api/NationOH.Api.csproj -c ${MODULE_NAME_PASCAL}DbContext"
echo "--------------------------------------------------"
echo "Remember to review all generated files and adjust namespaces, usings, and logic as per your project's specific conventions and needs."
echo "Ensure your main API project (startup project) references the newly created main module project ($MAIN_PROJECT_NAME.csproj)."
echo "--------------------------------------------------"

exit 0
