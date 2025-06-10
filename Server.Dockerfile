FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

RUN apt-get update \
    && apt-get install unzip \
    && curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l /vsdbg

WORKDIR /app

# Exclude .bin and .obj files when copying
COPY Server/ ./Server/
COPY Shared/ ./Shared/
RUN find ./Server -type d \( -name bin -o -name obj \) -exec rm -rf {} + || true
RUN find ./Shared -type d \( -name bin -o -name obj \) -exec rm -rf {} + || true

ENV ASPNETCORE_ENVIRONMENT=Development

WORKDIR /app/Server/WebApi

ENTRYPOINT ["dotnet", "watch", "run", "--project", "WebApi.csproj"]