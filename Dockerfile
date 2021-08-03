FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/SafeHouseAMS.Backend.Server/SafeHouseAMS.Backend.Server.csproj", "SafeHouseAMS.Backend.Server/"]
COPY ["src/SafeHouseAMS.BizLayer/SafeHouseAMS.BizLayer.csproj", "SafeHouseAMS.BizLayer/"]
COPY ["src/SafeHouseAMS.DataLayer/SafeHouseAMS.DataLayer.csproj", "SafeHouseAMS.DataLayer/"]
COPY ["src/SafeHouseAMS.Transport/SafeHouseAMS.Transport.csproj", "SafeHouseAMS.Transport/"]
RUN dotnet restore "/src/SafeHouseAMS.Backend.Server/SafeHouseAMS.Backend.Server.csproj"
COPY ["src/", "."]
WORKDIR "/src/SafeHouseAMS.Backend.Server"
RUN dotnet build "SafeHouseAMS.Backend.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SafeHouseAMS.Backend.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SafeHouseAMS.Backend.Server.dll"]
