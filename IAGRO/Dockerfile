FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5193

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["IAGRO.Api/IAGRO.Api.csproj", "IAGRO.Api/"]
COPY ["IAGRO.Application/IAGRO.Application.csproj", "IAGRO.Application/"]
COPY ["IAGRO.Domain/IAGRO.Domain.csproj", "IAGRO.Domain/"]
RUN dotnet restore "IAGRO.Api/IAGRO.Api.csproj"
COPY . .
WORKDIR "/src/IAGRO.Api"
RUN dotnet build "IAGRO.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IAGRO.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "IAGRO.Api.dll"]