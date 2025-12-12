# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar csproj y restaurar dependencias
COPY *.csproj ./
RUN dotnet restore

# Copiar el resto y construir
COPY . ./
RUN dotnet publish -c Release -o out

# Etapa runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./
EXPOSE 7151
ENTRYPOINT ["dotnet", "Coink.Usuarios.API.dll"]
