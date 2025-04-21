FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

RUN apt-get update \
    && apt-get install unzip \
    && curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l /vsdbg

WORKDIR /app

COPY Server/ ./Server/
COPY Shared/ ./Shared/

ENV ASPNETCORE_ENVIRONMENT=Development

WORKDIR /app/Server/WebApi

ENTRYPOINT ["dotnet", "watch", "run", "--project", "WebApi.csproj"]