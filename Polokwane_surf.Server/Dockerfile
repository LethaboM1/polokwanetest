# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copy project files
COPY . ./

# Restore dependencies
RUN dotnet restore

# Publish the app to the /out directory
RUN dotnet publish -c Release -o /out

# Use the ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# Copy published app from build stage
COPY --from=build /out ./

# Set environment variable for ASP.NET
ENV ASPNETCORE_URLS=http://+:8080

# Expose port (Render expects the app to listen on 8080)
EXPOSE 8080

# Run the app
ENTRYPOINT ["dotnet", "polokwane_surf.server.dll"]
