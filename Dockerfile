FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY employee_management_system.csproj ./
RUN dotnet restore employee_management_system.csproj

COPY . ./
RUN dotnet publish employee_management_system.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish ./

USER $APP_UID
ENTRYPOINT ["dotnet", "employee_management_system.dll"]
