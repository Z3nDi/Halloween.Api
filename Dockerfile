#  запуск програми 
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Образ для компіляції коду 
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копіює файл проекту і завантажує залежності
COPY ["Halloween.Api.csproj", "./"]
RUN dotnet restore "./Halloween.Api.csproj"

# Копіює весь інший код і компілює його
COPY . .
RUN dotnet build "Halloween.Api.csproj" -c Release -o /app/build

# Публікація оптимізованої версії
FROM build AS publish
RUN dotnet publish "Halloween.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Фінальна збірка
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Команда для запуску API
ENTRYPOINT ["dotnet", "Halloween.Api.dll"]