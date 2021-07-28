#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Leviathan.Api.Alpha/Leviathan.Api.Alpha.csproj", "Leviathan.Api.Alpha/"]
RUN dotnet restore "Leviathan.Api.Alpha/Leviathan.Api.Alpha.csproj"
COPY . .
WORKDIR "/src/Leviathan.Api.Alpha"
RUN dotnet build "Leviathan.Api.Alpha.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Leviathan.Api.Alpha.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Leviathan.Api.Alpha.dll"]