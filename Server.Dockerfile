FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG INCLUDE_DEBUGGER=false

WORKDIR /app

RUN if [ "$INCLUDE_DEBUGGER" = "true" ]; then \
        apt-get update \
        && apt-get install unzip \
        && curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l /vsdbg; \
    fi

COPY Server/ ./Server/
COPY Shared/ ./Shared/

WORKDIR /app/Server/WebApi
RUN dotnet restore 
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish

# Runtime image 
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS="http://+:8080"
EXPOSE 8080
ENTRYPOINT ["dotnet", "WebApi.dll"]