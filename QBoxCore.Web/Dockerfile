FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY QBoxCore.Web/QBoxCore.Web.csproj QBoxCore.Web/
COPY QBoxCore.Api.Client/QBoxCore.Api.Client.csproj QBoxCore.Api.Client/
COPY QBoxCore.Api.DTO/QBoxCore.Api.DTO.csproj QBoxCore.Api.DTO/
COPY QBoxCore.Logging/QBoxCore.Logging.csproj QBoxCore.Logging/
COPY QBoxCore.Common/QBoxCore.Common.csproj QBoxCore.Common/
RUN dotnet restore QBoxCore.Web/QBoxCore.Web.csproj
COPY . .
WORKDIR /src/QBoxCore.Web
RUN dotnet build QBoxCore.Web.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish QBoxCore.Web.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "QBoxCore.Web.dll"]