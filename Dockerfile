# Use a imagem base do .NET SDK
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CinemaApi.csproj", "./"]
RUN dotnet restore "./CinemaApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "CinemaApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CinemaApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CinemaApi.dll"]
