FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/MasterChef.Api/MasterChef.Api.csproj", "src/MasterChef.Api/"]
COPY ["src/MasterChef.Application/MasterChef.Application.csproj", "src/MasterChef.Application/"]
COPY ["src/MasterChef.Domain/MasterChef.Domain.csproj", "src/MasterChef.Domain/"]
COPY ["src/MasterChef.Infra/MasterChef.Infra.csproj", "src/MasterChef.Infra/"]
COPY ["src/MasterChef.Infra.Postgres/MasterChef.Infra.Postgres.csproj", "src/MasterChef.Infra.Postgres/"]
COPY ["src/MasterChef.Infra.Sqlite/MasterChef.Infra.Sqlite.csproj", "src/MasterChef.Infra.Sqlite/"]
COPY ["src/MasterChef.Infra.SqlServer/MasterChef.Infra.SqlServer.csproj", "src/MasterChef.Infra.SqlServer/"]
RUN dotnet restore "src/MasterChef.Api/MasterChef.Api.csproj"
COPY . .
WORKDIR "/src/src/MasterChef.Api"
RUN dotnet build "MasterChef.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MasterChef.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MasterChef.Api.dll"]
