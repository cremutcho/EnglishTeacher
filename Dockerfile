# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia tudo
COPY . .

# Restaura a solution (importante!)
RUN dotnet restore "EnglishTeacher.sln"

# Publica a API
RUN dotnet publish "EnglishTeacher.API/EnglishTeacher.API.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copia o publish
COPY --from=build /app/publish .

# Porta do Render
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

# Start da aplicação
ENTRYPOINT ["dotnet", "EnglishTeacher.API.dll"]