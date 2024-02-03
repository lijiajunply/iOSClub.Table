FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["iOSClub.Api/iOSClub.Api.csproj", "iOSClub.Api/"]
COPY ["iOSClub.Share/iOSClub.Share.csproj", "iOSClub.Share/"]
RUN dotnet restore "iOSClub.Api/iOSClub.Api.csproj"
COPY . .
WORKDIR "/src/iOSClub.Api"
RUN dotnet build "iOSClub.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "iOSClub.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "iOSClub.Api.dll"]
