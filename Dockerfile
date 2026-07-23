FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS base
WORKDIR /app
EXPOSE 8080

USER root
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
USER app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG GITHUB_USERNAME
ARG GITHUB_PASSWORD

WORKDIR /src

COPY ["Services.Presentation/Services.Presentation.csproj", "Services.Presentation/"]
COPY ["Services.Application/Services.Application.csproj", "Services.Application/"]
COPY ["Services.Infrastructure/Services.Infrastructure.csproj", "Services.Infrastructure/"]
COPY ["Services.Domain/Services.Domain.csproj", "Services.Domain/"]
COPY ["nuget.config", "./"]

RUN dotnet nuget update source "github" \
    --username "$GITHUB_USERNAME" \
    --password "$GITHUB_PASSWORD" \
    --store-password-in-clear-text \
    --configfile nuget.config

RUN dotnet restore "./Services.Presentation/Services.Presentation.csproj"

COPY . .
WORKDIR "/src/Services.Presentation"
RUN dotnet build "./Services.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Services.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Services.Presentation.dll"]