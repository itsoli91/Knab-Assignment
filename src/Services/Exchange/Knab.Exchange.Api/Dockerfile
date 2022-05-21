#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Exchange/Knab.Exchange.Api/Knab.Exchange.Api.csproj", "src/Services/Exchange/Knab.Exchange.Api/"]
COPY ["src/Services/Exchange/Knab.Exchange.Infrastructure/Knab.Exchange.Infrastructure.csproj", "src/Services/Exchange/Knab.Exchange.Infrastructure/"]
COPY ["src/Services/Exchange/Knab.Exchange.Domain/Knab.Exchange.Domain.csproj", "src/Services/Exchange/Knab.Exchange.Domain/"]
RUN dotnet restore "src/Services/Exchange/Knab.Exchange.Api/Knab.Exchange.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Exchange/Knab.Exchange.Api"
RUN dotnet build "Knab.Exchange.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Knab.Exchange.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Knab.Exchange.Api.dll"]