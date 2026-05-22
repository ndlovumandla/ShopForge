FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["src/ShopForge.Api/ShopForge.Api.csproj", "src/ShopForge.Api/"]
COPY ["src/ShopForge.Database/ShopForge.Database.csproj", "src/ShopForge.Database/"]
COPY ["src/ShopForge.Shared/ShopForge.Shared.csproj", "src/ShopForge.Shared/"]
RUN dotnet restore "src/ShopForge.Api/ShopForge.Api.csproj"

COPY . .
WORKDIR "/src/src/ShopForge.Api"
RUN dotnet build "ShopForge.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShopForge.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShopForge.Api.dll"]
