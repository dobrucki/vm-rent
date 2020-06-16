FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build
WORKDIR /app

COPY . .
RUN dotnet restore RentingService.Api
RUN dotnet publish RentingService.Api -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "RentingService.Api.dll"]