#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Lets get all the project files
COPY Microservices.SampleWork.sln Microservices.SampleWork.sln
COPY src/AuctionService/AuctionService.csproj src/AuctionService/
COPY src/SearchService/SearchService.csproj src/SearchService/
COPY src/GatewayService/GatewayService.csproj src/GatewayService/
COPY src/Contracts/Contracts.csproj src/Contracts/
COPY src/IdentityService/IdentityService.csproj src/IdentityService/
COPY src/BiddingService/BiddingService.csproj src/BiddingService/

# NuGet Restore
RUN dotnet restore ./Microservices.SampleWork.sln

# Copy app folders
COPY . ./

FROM build AS publish

RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuctionService.dll"]