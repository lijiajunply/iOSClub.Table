FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["iOSClub.Table/iOSClub.Table.csproj", "iOSClub.Table/"]
RUN dotnet restore "iOSClub.Table/iOSClub.Table.csproj"
COPY . .
WORKDIR "/src/iOSClub.Table"
RUN dotnet build "iOSClub.Table.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "iOSClub.Table.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "iOSClub.Table.dll"]
