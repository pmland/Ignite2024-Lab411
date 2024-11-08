# Use the official .NET 8 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project files and restore any dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the remaining files and build the app
COPY . .
RUN dotnet publish -c Release -o out

# Use the official .NET 8 runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the build output
COPY --from=build /app/out ./

# Copy the required directories into the container
COPY ./cpu_and_mobile/cpu-int4-rtn-block-32 /app/cpu_and_mobile/cpu-int4-rtn-block-32

# Expose the port the app runs on
EXPOSE 5109

# Set the environment variable for ASP.NET Core to listen on port 5045
ENV ASPNETCORE_URLS=http://+:5109

# Run the app
ENTRYPOINT ["dotnet", "fashion-store-backend.dll"]
