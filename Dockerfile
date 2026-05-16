# ===============================
# BUILD
# ===============================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar archivos de proyecto
COPY ["GestionG.Solution.slnx", "./"]
COPY ["GestionG.Api/GestionG.Api.csproj", "GestionG.Api/"]
COPY ["GestionG.Application/GestionG.Application.csproj", "GestionG.Application/"]
COPY ["GestionG.Domain/GestionG.Domain.csproj", "GestionG.Domain/"]
COPY ["GestionG.Infrastructure/GestionG.Infrastructure.csproj", "GestionG.Infrastructure/"]

# Restaurar dependencias
RUN dotnet restore "GestionG.Solution.slnx"

# Copiar todo el código y publicar
COPY . .

# Publicar la API
WORKDIR "/src/GestionG.Api"
RUN dotnet publish "GestionG.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ===============================
# RUNTIME
# ===============================
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

# Librerías necesarias para la conexión a base de datos
RUN apt-get update && apt-get install -y libgssapi-krb5-2 && rm -rf /var/lib/apt/lists/*

# Copiar archivos publicados
COPY --from=build /app/publish .

# Render usa variable PORT automáticamente
ENV ASPNETCORE_URLS=http://+:${PORT}
EXPOSE 8080

ENTRYPOINT ["dotnet", "GestionG.Api.dll"]