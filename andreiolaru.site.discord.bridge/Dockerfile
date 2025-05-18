FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY *.sln ./
COPY andreiolaru.site.discord.bridge/*.csproj ./andreiolaru.site.discord.bridge/
COPY andreiolaru.site.discord.bridge.Application/*.csproj ./andreiolaru.site.discord.bridge.Application/
COPY andreiolaru.site.discord.bridge.Domain/*.csproj ./andreiolaru.site.discord.bridge.Domain/
COPY andreiolaru.site.discord.bridge.Persistence/*.csproj ./andreiolaru.site.discord.bridge.Persistence/
COPY andreiolaru.site.discord.bridge.Infrastructure/*.csproj ./andreiolaru.site.discord.bridge.Infrastructure/

RUN dotnet restore

COPY . .
WORKDIR /src/andreiolaru.site.discord.bridge
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "andreiolaru.site.discord.bridge.dll"]
