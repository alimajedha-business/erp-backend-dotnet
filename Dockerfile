# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["Src/API/API/NGErp.API.csproj", "Src/API/API/"]
COPY ["Src/BaseModule/Base.API/NGErp.Base.API.csproj", "Src/BaseModule/Base.API/"]
COPY ["Src/BaseModule/Base.Service/NGErp.Base.Service.csproj", "Src/BaseModule/Base.Service/"]
COPY ["Src/BaseModule/Base.Infrastructure/NGErp.Base.Infrastructure.csproj", "Src/BaseModule/Base.Infrastructure/"]
COPY ["Src/BaseModule/Base.Domain/NGErp.Base.Domain.csproj", "Src/BaseModule/Base.Domain/"]
COPY ["Src/GeneralModule/General.API/NGErp.General.API.csproj", "Src/GeneralModule/General.API/"]
COPY ["Src/GeneralModule/General.Service/NGErp.General.Service.csproj", "Src/GeneralModule/General.Service/"]
COPY ["Src/GeneralModule/General.Infrastructure/NGErp.General.Infrastructure.csproj", "Src/GeneralModule/General.Infrastructure/"]
COPY ["Src/GeneralModule/General.Domain/NGErp.General.Domain.csproj", "Src/GeneralModule/General.Domain/"]

RUN dotnet restore "Src/API/API/NGErp.API.csproj"

# Copy all source files
COPY . .

# Build the application
WORKDIR "/src/Src/API/API"
RUN dotnet build "NGErp.API.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "NGErp.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Install curl for health checks
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

COPY --from=publish /app/publish .

# Create logs directory
RUN mkdir -p /app/logs

EXPOSE 8080

HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
  CMD curl -f http://localhost:8080/metrics || exit 1

ENTRYPOINT ["dotnet", "NGErp.API.dll"]
