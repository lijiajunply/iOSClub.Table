FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["iOSClub.Api/iOSClub.Api.csproj", "iOSClub.Api/"]
RUN dotnet restore "iOSClub.Api/iOSClub.Api.csproj"
COPY . .
WORKDIR "/src/iOSClub.Api"
RUN dotnet build "iOSClub.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "iOSClub.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "iOSClub.Api.dll"]
