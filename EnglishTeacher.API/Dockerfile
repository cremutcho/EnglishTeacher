# =========================
# 🔹 Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia tudo
COPY . ./

# Restaura e publica o projeto correto
RUN dotnet restore "EnglishTeacher.API/EnglishTeacher.API.csproj"
RUN dotnet publish "EnglishTeacher.API/EnglishTeacher.API.csproj" -c Release -o out

# =========================
# 🔹 Runtime stage
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out .

# Porta do Render
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "EnglishTeacher.API.dll"]