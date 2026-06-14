# ==========================
# BUILD
# ==========================

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# CORREGIDO: Se agregaron los dos argumentos (origen y destino)
# Copiar todo el proyecto al contenedor para poder restaurar y compilar
COPY . .

# Restaurar dependencias usando la solución .slnx
RUN dotnet restore "GestionG.Solution.slnx"

# Publicar la API (Cambiado a la ruta correcta basada en el COPY . .)
WORKDIR "/src/GestionG.Api"
RUN dotnet publish "GestionG.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ==========================
# RUNTIME
# ==========================

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

# Librerías necesarias (PostgreSQL / seguridad)
RUN apt-get update && apt-get install -y libgssapi-krb5-2 && rm -rf /var/lib/apt/lists/*

# Copiar la app publicada desde la etapa de compilación
COPY --from=build /app/publish .

# Render usa el puerto dinámico asignado en la variable $PORT
ENV ASPNETCORE_URLS=http://+:${PORT}

# Exponer puerto (referencial)
EXPOSE 8080

# Ejecutar la API
ENTRYPOINT ["dotnet", "GestionG.Api.dll"]