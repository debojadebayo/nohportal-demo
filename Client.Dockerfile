FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app

COPY Client/Client.sln ./Client/
COPY ./Client/Client.csproj ./Client/
COPY ./Shared/Shared.csproj ./Shared/

RUN dotnet restore ./Client/Client.sln
COPY . .
WORKDIR /app/Client
RUN dotnet publish "./Client.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM nginx:stable-alpine
WORKDIR /app
EXPOSE 8080
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html