FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
EXPOSE 9876/udp
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY src/ .
RUN dotnet restore server/MultiplayerServer.csproj
RUN dotnet build server/MultiplayerServer.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish server/MultiplayerServer.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT dotnet MultiplayerServer.dll