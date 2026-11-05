FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY FactureGenerator.csproj ./
RUN dotnet restore FactureGenerator.csproj

COPY . ./
RUN dotnet publish FactureGenerator.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# L'image aspnet:10.0 ecoute deja sur 8080 via ASPNETCORE_HTTP_PORTS
EXPOSE 8080

COPY --from=build /app/publish ./

USER $APP_UID
ENTRYPOINT ["dotnet", "FactureGenerator.dll"]
