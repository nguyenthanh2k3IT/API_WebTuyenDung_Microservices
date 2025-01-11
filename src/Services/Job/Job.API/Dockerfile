#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Job/Job.API/Job.API.csproj", "Services/Job/Job.API/"]
COPY ["Services/Job/Job.Domain/Job.Domain.csproj", "Services/Job/Job.Domain/"]
COPY ["Services/Job/Job.Application/Job.Application.csproj", "Services/Job/Job.Application/"]
COPY ["Services/Job/Job.Infrastructure/Job.Infrastructure.csproj", "Services/Job/Job.Infrastructure/"]
RUN dotnet restore "./Services/Job/Job.API/Job.API.csproj"
COPY . .
WORKDIR "/src/Services/Job/Job.API"
RUN dotnet build "./Job.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Job.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Job.API.dll"]