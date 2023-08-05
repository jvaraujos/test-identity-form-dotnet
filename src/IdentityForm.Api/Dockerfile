#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
RUN sed -i "s|DEFAULT@SECLEVEL=2|DEFAULT@SECLEVEL=1|g" /etc/ssl/openssl.cnf

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src 
RUN dotnet dev-certs https --trust
RUN apt-get update && apt-get install -y --no-install-recommends libgdiplus libc6-dev && apt-get clean && rm -rf /var/lib/apt/lists/*
COPY ["src/IdentityForm.Api/IdentityForm.Api.csproj", "src/IdentityForm.Api/"]
COPY ["src/IdentityForm.Application/IdentityForm.Application.csproj", "src/IdentityForm.Application/"]
COPY ["src/IdentityForm.Domain/IdentityForm.Domain.csproj", "src/IdentityForm.Domain/"]
COPY ["src/IdentityForm.Infrastructure.Shared/IdentityForm.Infrastructure.Shared.csproj", "src/IdentityForm.Infrastructure.Shared/"]
COPY ["src/IdentityForm.Shared/IdentityForm.Shared.csproj", "src/IdentityForm.Shared/"]
COPY ["src/IdentityForm.Infrastructure/IdentityForm.Infrastructure.csproj", "src/IdentityForm.Infrastructure/"]
RUN dotnet restore "src/IdentityForm.Api/IdentityForm.Api.csproj"
COPY . .
WORKDIR "src/IdentityForm.Api/"
RUN dotnet build "IdentityForm.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityForm.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityForm.Api.dll"]