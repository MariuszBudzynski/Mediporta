# Użyj obrazu SDK .NET 5.0 jako środowiska budowania
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

# Skopiuj pliki csproj i przywróć zależności
COPY Mediporta.Common/*.csproj ./Mediporta.Common/
COPY Mediporta.Tests/*.csproj ./Mediporta.Tests/
COPY Mediporta/*.csproj ./Mediporta/
RUN dotnet restore

# Skopiuj wszystko inne i zbuduj aplikację
COPY . ./
RUN dotnet publish -c Release -o out

# Użyj obrazu ASP.NET 5.0 jako środowiska uruchomieniowego
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "Mediporta.dll"]
