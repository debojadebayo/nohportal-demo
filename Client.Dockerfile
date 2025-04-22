FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

RUN apt-get update \
    && apt-get install unzip \
    && curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l /vsdbg

WORKDIR /app

COPY Client/ ./Client/
COPY Shared/ ./Shared/

ENV ASPNETCORE_ENVIRONMENT=Development

WORKDIR /app/Client

ENTRYPOINT ["dotnet", "watch", "run", "--project", "Client.csproj"]