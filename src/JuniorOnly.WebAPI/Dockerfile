FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copier le fichier solution d'abord
COPY ["JuniorOnly.sln", "./"]

# Copier tous les fichiers .csproj
COPY ["src/JuniorOnly.WebAPI/JuniorOnly.WebAPI.csproj", "src/JuniorOnly.WebAPI/"]
COPY ["src/JuniorOnly.Application/JuniorOnly.Application.csproj", "src/JuniorOnly.Application/"]
COPY ["src/JuniorOnly.Domain/JuniorOnly.Domain.csproj", "src/JuniorOnly.Domain/"]
COPY ["src/JuniorOnly.Infrastructure/JuniorOnly.Infrastructure.csproj", "src/JuniorOnly.Infrastructure/"]

# Restaurer les dépendances (utilise la solution pour restaurer tous les projets)
RUN dotnet restore "JuniorOnly.sln"

# Copier tout le code source
COPY . .

# Build du projet WebAPI
WORKDIR "/src/src/JuniorOnly.WebAPI"
RUN dotnet build "JuniorOnly.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "JuniorOnly.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JuniorOnly.WebAPI.dll"]
