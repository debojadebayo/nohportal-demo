FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 5003
EXPOSE 5435

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY Server/Server.sln ./Server/

COPY Server/WebApi/WebApi.csproj ./Server/WebApi/

COPY Server/Base/Endpoints/ComposedHealthBase.Server.Endpoints.csproj ./Server/Base/Endpoints/
COPY Server/Base/Entities/ComposedHealthBase.Server.Entities.csproj ./Server/Base/Entities/
COPY Server/Base/Infrastructure/ComposedHealthBase.Server.csproj ./Server/Base/Infrastructure/
COPY Server/Base/Services/ComposedHealthBase.Server.Services.csproj ./Server/Base/Services/
COPY Server/Base/Application/ComposedHealthBase.Server.Application.csproj ./Server/Base/Application/

COPY Server/Modules/CRM/Endpoints/Server.Modules.CRM.Endpoints.csproj ./Server/Modules/CRM/Endpoints/
COPY Server/Modules/CRM/Entities/Server.Modules.CRM.Entities.csproj ./Server/Modules/CRM/Entities/
COPY Server/Modules/CRM/Infrastructure/Server.Modules.CRM.Infrastructure.csproj ./Server/Modules/CRM/Infrastructure/
COPY Server/Modules/CRM/Services/Server.Modules.CRM.Services.csproj ./Server/Modules/CRM/Services/
COPY Server/Modules/CRM/Application/Server.Modules.CRM.Application.csproj ./Server/Modules/CRM/Application/

COPY Server/Modules/Scheduling/Endpoints/Server.Modules.Scheduling.Endpoints.csproj ./Server/Modules/Scheduling/Endpoints/
COPY Server/Modules/Scheduling/Entities/Server.Modules.Scheduling.Entities.csproj ./Server/Modules/Scheduling/Entities/
COPY Server/Modules/Scheduling/Infrastructure/Server.Modules.Scheduling.Infrastructure.csproj ./Server/Modules/Scheduling/Infrastructure/
COPY Server/Modules/Scheduling/Services/Server.Modules.Scheduling.Services.csproj ./Server/Modules/Scheduling/Services/
COPY Server/Modules/Scheduling/Application/Server.Modules.Scheduling.Application.csproj ./Server/Modules/Scheduling/Application/

COPY Shared/Shared.csproj ./Shared/

RUN dotnet restore ./Server/Server.sln
COPY . .
WORKDIR /src/Server/WebApi
RUN dotnet build "./WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]