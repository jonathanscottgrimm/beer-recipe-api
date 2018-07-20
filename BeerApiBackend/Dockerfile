FROM microsoft/aspnetcore:2.0-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-nanoserver-1709 AS build
WORKDIR /src
COPY BeerApiBackend/BeerApiBackend.csproj BeerApiBackend/
RUN dotnet restore BeerApiBackend/BeerApiBackend.csproj
COPY . .
WORKDIR /src/BeerApiBackend
RUN dotnet build BeerApiBackend.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish BeerApiBackend.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BeerApiBackend.dll"]
