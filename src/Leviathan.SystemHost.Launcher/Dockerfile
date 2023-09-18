#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Leviathan.SystemHost.Launcher/Leviathan.SystemHost.Launcher.csproj", "Leviathan.SystemHost.Launcher/"]
RUN dotnet restore "Leviathan.SystemHost.Launcher/Leviathan.SystemHost.Launcher.csproj"
COPY . .
WORKDIR "/src/Leviathan.SystemHost.Launcher"
RUN dotnet build "Leviathan.SystemHost.Launcher.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "Leviathan.SystemHost.Launcher.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Leviathan.SystemHost.Launcher.dll"]