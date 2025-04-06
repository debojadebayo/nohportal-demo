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

COPY Server/Base/Endpoints/ComposedHealthBase.Server.BaseModule.Endpoints.csproj ./Server/Base/Endpoints/
COPY Server/Base/Entities/ComposedHealthBase.Server.BaseModule.Entities.csproj ./Server/Base/Entities/
COPY Server/Base/Infrastructure/ComposedHealthBase.Server.BaseModule.Infrastructure.csproj ./Server/Base/Infrastructure/
COPY Server/Base/Services/ComposedHealthBase.Server.BaseModule.Services.csproj ./Server/Base/Services/
COPY Server/Base/Application/ComposedHealthBase.Server.BaseModule.Application.csproj ./Server/Base/Application/

COPY Server/Modules/Billing/Endpoints/Server.Modules.Billing.Endpoints.csproj ./Server/Modules/Billing/Endpoints/
COPY Server/Modules/Billing/Entities/Server.Modules.Billing.Entities.csproj ./Server/Modules/Billing/Entities/
COPY Server/Modules/Billing/Infrastructure/Server.Modules.Billing.Infrastructure.csproj ./Server/Modules/Billing/Infrastructure/
COPY Server/Modules/Billing/Services/Server.Modules.Billing.Services.csproj ./Server/Modules/Billing/Services/
COPY Server/Modules/Billing/Application/Server.Modules.Billing.Application.csproj ./Server/Modules/Billing/Application/

COPY Server/Modules/CRM/Endpoints/Server.Modules.CRM.Endpoints.csproj ./Server/Modules/CRM/Endpoints/
COPY Server/Modules/CRM/Entities/Server.Modules.CRM.Entities.csproj ./Server/Modules/CRM/Entities/
COPY Server/Modules/CRM/Infrastructure/Server.Modules.CRM.Infrastructure.csproj ./Server/Modules/CRM/Infrastructure/
COPY Server/Modules/CRM/Services/Server.Modules.CRM.Services.csproj ./Server/Modules/CRM/Services/
COPY Server/Modules/CRM/Application/Server.Modules.CRM.Application.csproj ./Server/Modules/CRM/Application/

COPY Server/Modules/Clinical/Endpoints/Server.Modules.Clinical.Endpoints.csproj ./Server/Modules/Clinical/Endpoints/
COPY Server/Modules/Clinical/Entities/Server.Modules.Clinical.Entities.csproj ./Server/Modules/Clinical/Entities/
COPY Server/Modules/Clinical/Infrastructure/Server.Modules.Clinical.Infrastructure.csproj ./Server/Modules/Clinical/Infrastructure/
COPY Server/Modules/Clinical/Services/Server.Modules.Clinical.Services.csproj ./Server/Modules/Clinical/Services/
COPY Server/Modules/Clinical/Application/Server.Modules.Clinical.Application.csproj ./Server/Modules/Clinical/Application/

COPY Server/Modules/Schedule/Endpoints/Server.Modules.Schedule.Endpoints.csproj ./Server/Modules/Schedule/Endpoints/
COPY Server/Modules/Schedule/Entities/Server.Modules.Schedule.Entities.csproj ./Server/Modules/Schedule/Entities/
COPY Server/Modules/Schedule/Infrastructure/Server.Modules.Schedule.Infrastructure.csproj ./Server/Modules/Schedule/Infrastructure/
COPY Server/Modules/Schedule/Services/Server.Modules.Schedule.Services.csproj ./Server/Modules/Schedule/Services/
COPY Server/Modules/Schedule/Application/Server.Modules.Schedule.Application.csproj ./Server/Modules/Schedule/Application/

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