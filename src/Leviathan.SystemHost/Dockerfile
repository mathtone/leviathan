#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /src
COPY ["Leviathan.SystemHost/Leviathan.SystemHost.csproj", "Leviathan.SystemHost/"]
RUN dotnet restore "Leviathan.SystemHost/Leviathan.SystemHost.csproj"
COPY . .
WORKDIR "/src/Leviathan.SystemHost"
RUN dotnet build "Leviathan.SystemHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Leviathan.SystemHost.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Leviathan.SystemHost.dll"]