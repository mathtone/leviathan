#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Sanbox.TestAPI/Sanbox.TestAPI.csproj", "Sanbox.TestAPI/"]
RUN dotnet restore "Sanbox.TestAPI/Sanbox.TestAPI.csproj"
COPY . .
WORKDIR "/src/Sanbox.TestAPI"
RUN dotnet build "Sanbox.TestAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sanbox.TestAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sanbox.TestAPI.dll"]