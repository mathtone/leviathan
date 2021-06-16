#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Leviathan.Alpha.Api/Leviathan.Alpha.Api.csproj", "Leviathan.Alpha.Api/"]
RUN dotnet restore "Leviathan.Alpha.Api/Leviathan.Alpha.Api.csproj"
COPY . .
WORKDIR "/src/Leviathan.Alpha.Api"
RUN dotnet build "Leviathan.Alpha.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Leviathan.Alpha.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Leviathan.Alpha.Api.dll"]