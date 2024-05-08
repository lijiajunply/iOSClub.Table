FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["iOSClub.Table/iOSClub.Table.csproj", "iOSClub.Table/"]
COPY ["iOSClub.Share/iOSClub.Share.csproj", "iOSClub.Share/"]
RUN dotnet restore "iOSClub.Table/iOSClub.Table.csproj"
COPY . .
WORKDIR "/src/iOSClub.Table"
RUN dotnet build "iOSClub.Table.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "iOSClub.Table.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
ENV SQL=""
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "iOSClub.Table.dll"]
