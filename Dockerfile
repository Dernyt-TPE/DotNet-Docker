FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

COPY . .

WORKDIR /StoreApp

RUN dotnet restore 

RUN dotnet publish "StoreApp/StoreApp.csproj" -c Release -o /app

# RUN dotnet test --logger "trx;LogFileName=./sampledevops.trx"

FROM mcr.microsoft.com/dotnet/aspnet:7.0

COPY --from=builder /app .

ENTRYPOINT ["dotnet", "StoreApp.dll"]