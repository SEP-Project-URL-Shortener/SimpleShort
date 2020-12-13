# Offical dotnet image as the base environment
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base

# Change Diectory to /app
WORKDIR /app

# Expose port 80
EXPOSE 80

# Expose port 443
EXPOSE 443

# Offical dotnet image as the build environment
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build

WORKDIR /src

# Copy only the csproj to the current dir
COPY *.csproj .

# Dotnet restore the proj
RUN dotnet restore

# Copy the rest of the files into the dir
COPY . .

# Change Diectory to /src/
WORKDIR "/src/."

# Dotnet build project as release. Output into /app/build
RUN dotnet build *.csproj -c Release -o /app/build

# From build environment create publish environment
FROM build AS publish

# Dotnet publish project as release. Output into /app/build
RUN dotnet publish *.csproj -c Release -o /app/publish

# From base environment create final environment
FROM base AS final

# Change Diectory to /app
WORKDIR /app

# Copy from publish environment, /app/publish into final environment /app
COPY --from=publish /app/publish .

# Dotnet run SimpleShort.dll
ENTRYPOINT ["dotnet", "SimpleShort.dll"]
