# Use the official .NET SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the csproj file and restore any dependencies
COPY ["dotnetbackend.csproj", "./"]
RUN dotnet restore "./dotnetbackend.csproj"

# Copy the rest of the application code
COPY . .
WORKDIR "/src/."
RUN dotnet build "dotnetbackend.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "dotnetbackend.csproj" -c Release -o /app/publish

# Use the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "dotnetbackend.dll"]
