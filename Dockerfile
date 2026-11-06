FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Restauration en couche separee : seuls les .csproj (et la solution) sont copies.
COPY FactureGenerator.sln ./
COPY src/FactureGenerator.Domain/FactureGenerator.Domain.csproj src/FactureGenerator.Domain/
COPY src/FactureGenerator.Application/FactureGenerator.Application.csproj src/FactureGenerator.Application/
COPY src/FactureGenerator.Infrastructure/FactureGenerator.Infrastructure.csproj src/FactureGenerator.Infrastructure/
COPY src/FactureGenerator.Web/FactureGenerator.Web.csproj src/FactureGenerator.Web/
RUN dotnet restore FactureGenerator.sln

COPY . ./
RUN dotnet publish src/FactureGenerator.Web/FactureGenerator.Web.csproj -c Release -o /app/publish /p:UseAppHost=false

# Bundle de migrations : executable autonome (equivalent de `dotnet ef database update`)
# Conserve la version de dotnet-ef alignee sur Microsoft.EntityFrameworkCore.Design du projet Infrastructure.
FROM build AS ef-bundle
RUN dotnet tool install --global dotnet-ef --version 10.0.14
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet ef migrations bundle --project src/FactureGenerator.Infrastructure/FactureGenerator.Infrastructure.csproj --configuration Release --output /app/efbundle

# Image dediee aux migrations, lancee a la demande (voir service `migrations` dans docker-compose.yml)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS migrations
WORKDIR /app
COPY --from=ef-bundle /app/efbundle ./

USER $APP_UID
ENTRYPOINT ["./efbundle"]

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# L'image aspnet:10.0 ecoute deja sur 8080 via ASPNETCORE_HTTP_PORTS
EXPOSE 8080

COPY --from=build /app/publish ./

USER $APP_UID
ENTRYPOINT ["dotnet", "FactureGenerator.Web.dll"]
