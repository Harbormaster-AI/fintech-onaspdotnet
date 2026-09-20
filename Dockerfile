FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY fintechonaspdotnet/Api.csproj fintechonaspdotnet/
RUN dotnet restore fintechonaspdotnet/Api.csproj

COPY fintechonaspdotnet/ fintechonaspdotnet/

RUN dotnet publish fintechonaspdotnet/Api.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Api.dll"]