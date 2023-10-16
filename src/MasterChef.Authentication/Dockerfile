FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
RUN dotnet dev-certs https
WORKDIR /src
COPY ["src/MasterChef.Authentication/MasterChef.Authentication.csproj", "src/MasterChef.Authentication/"]
COPY ["src/MasterChef.Application/MasterChef.Application.csproj", "src/MasterChef.Application/"]
COPY ["src/MasterChef.Domain/MasterChef.Domain.csproj", "src/MasterChef.Domain/"]
COPY ["src/MasterChef.Infra/MasterChef.Infra.csproj", "src/MasterChef.Infra/"]
COPY ["src/MasterChef.Dto/MasterChef.Dto.csproj", "src/MasterChef.Dto/"]
COPY ["src/MasterChef.Infra.Sqlite/MasterChef.Infra.Sqlite.csproj", "src/MasterChef.Infra.Sqlite/"]
COPY ["src/MasterChef.Infra.SqlServer/MasterChef.Infra.SqlServer.csproj", "src/MasterChef.Infra.SqlServer/"]
RUN dotnet restore "src/MasterChef.Authentication/MasterChef.Authentication.csproj"
COPY . .
WORKDIR "/src/src/MasterChef.Authentication"
RUN dotnet build "MasterChef.Authentication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MasterChef.Authentication.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MasterChef.Authentication.dll"]
