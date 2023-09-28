#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["HotelManagement.API/HotelManagement.API.csproj", "HotelManagement.API/"]
COPY ["HotelManagement.Infrastructure/HotelManagement.Infrastructure.csproj", "HotelManagement.Infrastructure/"]
RUN dotnet restore "HotelManagement.API/HotelManagement.API.csproj"
COPY . .
WORKDIR "/src/HotelManagement.API"
RUN dotnet build "HotelManagement.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HotelManagement.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HotelManagement.API.dll"]