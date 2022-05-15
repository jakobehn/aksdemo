FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["QBoxCore.HighscoreNotification/QBoxCore.HighscoreNotification.csproj", "QBoxCore.HighscoreNotification/"]
RUN dotnet restore "QBoxCore.HighscoreNotification/QBoxCore.HighscoreNotification.csproj"
COPY . .
WORKDIR "/src/QBoxCore.HighscoreNotification"
RUN dotnet build "QBoxCore.HighscoreNotification.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "QBoxCore.HighscoreNotification.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "QBoxCore.HighscoreNotification.dll"]