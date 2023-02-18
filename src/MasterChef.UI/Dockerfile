FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/MasterChef.UI/MasterChef.UI.csproj", "src/MasterChef.UI/"]
COPY ["src/MasterChef.Infra/MasterChef.Infra.csproj", "src/MasterChef.Infra/"]
COPY ["src/MasterChef.Domain/MasterChef.Domain.csproj", "src/MasterChef.Domain/"]
RUN dotnet restore "src/MasterChef.UI/MasterChef.UI.csproj"
COPY . .
WORKDIR "/src/src/MasterChef.UI"
RUN dotnet build "MasterChef.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MasterChef.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MasterChef.UI.dll"]
