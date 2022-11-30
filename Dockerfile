#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
RUN apt update 
RUN apt install curl iputils-ping -y
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BelajarWebApi.csproj", "."]
RUN dotnet restore "./BelajarWebApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "BelajarWebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BelajarWebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BelajarWebApi.dll"]