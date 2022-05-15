FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY QBoxCore.Api/QBoxCore.Api.csproj QBoxCore.Api/
COPY QBoxCore.Api.DTO/QBoxCore.Api.DTO.csproj QBoxCore.Api.DTO/
COPY QBoxCore.Logging/QBoxCore.Logging.csproj QBoxCore.Logging/
COPY QBoxCore.Common/QBoxCore.Common.csproj QBoxCore.Common/
RUN dotnet restore QBoxCore.Api/QBoxCore.Api.csproj
COPY . .
WORKDIR /src/QBoxCore.Api
RUN dotnet build QBoxCore.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish QBoxCore.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "QBoxCore.Api.dll"]
