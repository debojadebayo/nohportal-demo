FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Debug
ARG INCLUDE_DEBUGGER=true

WORKDIR /app

# Install debugger tools
RUN if [ "$INCLUDE_DEBUGGER" = "true" ]; then \
        apt-get update \
        && apt-get install -y unzip \
        && curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l /vsdbg; \
    fi

COPY Server/ ./Server/
COPY Shared/ ./Shared/
RUN find ./Server -type d \( -name bin -o -name obj \) -exec rm -rf {} + || true
RUN find ./Shared -type d \( -name bin -o -name obj \) -exec rm -rf {} + || true

WORKDIR /app/Server
RUN dotnet restore Server.sln
RUN dotnet publish WebApi/WebApi.csproj -c $BUILD_CONFIGURATION -o /app/publish

# Runtime image - use SDK for debugging capabilities
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS runtime
WORKDIR /app

# Install debugger tools in runtime image
RUN apt-get update \
    && apt-get install -y unzip \
    && curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l /vsdbg \
    && rm -rf /var/lib/apt/lists/*

# Copy published application
COPY --from=build /app/publish ./

# Set environment variables
ENV ASPNETCORE_URLS="http://+:8080"
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_USE_POLLING_FILE_WATCHER=1
ENV DOTNET_RUNNING_IN_CONTAINER=true

EXPOSE 8080

# Use the DLL from the WebApi project
ENTRYPOINT ["dotnet", "WebApi.dll"]