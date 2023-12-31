﻿#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 80

# Lets get all the project files
COPY Microservices.SampleWork.sln Microservices.SampleWork.sln
COPY src/AuctionService/AuctionService.csproj src/AuctionService/
COPY src/SearchService/SearchService.csproj src/SearchService/
COPY src/GatewayService/GatewayService.csproj src/GatewayService/
COPY src/Contracts/Contracts.csproj src/Contracts/
COPY src/IdentityService/IdentityService.csproj src/IdentityService/
COPY src/BiddingService/BiddingService.csproj src/BiddingService/
COPY src/NotificationService/NotificationService.csproj src/NotificationService/

# NuGet Restore
RUN dotnet restore ./Microservices.SampleWork.sln

# Copy app folders
COPY src/IdentityService src/IdentityService
WORKDIR /app/src/IdentityService

RUN dotnet publish  -c Release -o /app/src/out /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT ["dotnet", "IdentityService.dll"]
