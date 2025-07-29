# Use the official .NET 8.0 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copy csproj file(s) and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy all files and publish the app
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out .

EXPOSE 7059

ENTRYPOINT ["dotnet", "polokwane_surf.server.dll"]
