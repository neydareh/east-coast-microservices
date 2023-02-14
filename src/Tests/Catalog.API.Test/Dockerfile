#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Tests/Catalog.API.Test/Catalog.API.Test.csproj", "Tests/Catalog.API.Test/"]
COPY ["Services/Catalog/Catalog.API/Catalog.API.csproj", "Services/Catalog/Catalog.API/"]
RUN dotnet restore "Tests/Catalog.API.Test/Catalog.API.Test.csproj"
COPY . .
WORKDIR "/src/Tests/Catalog.API.Test"
RUN dotnet build "Catalog.API.Test.csproj" -c Release -o /app/build
RUN dotnet test "Catalog.API.Test.csproj"

# FROM build AS publish
# RUN dotnet publish "Catalog.API.Test.csproj" -c Release -o /app/publish /p:UseAppHost=false

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "Catalog.API.Test.dll"]