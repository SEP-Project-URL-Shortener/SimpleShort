# Dockerfile used to create image of project. WORK IN PROGRESS

# LAST ERROR: CAN NOT FIND COMPUTE PACKAGE. CREATE DOCKER IMAGE TO BUILD PACKAGE AS WELL
# Christopher Walters@CHRWALTEWORKSTA  ~\source\repos\SimpleShort\SimpleShort   master ≣ +2 ~5 -0 !          [22:53]
# ❯ docker build -t simpleshort:1.0.0.3 .
# [+] Building 7.4s (11/15)
#  => [internal] load .dockerignore                                                                                  0.0s
#  => => transferring context: 2B                                                                                    0.0s
#  => [internal] load build definition from Dockerfile                                                               0.0s
#  => => transferring dockerfile: 784B                                                                               0.0s
#  => [internal] load metadata for mcr.microsoft.com/dotnet/core/aspnet:3.1                                          1.0s
#  => [internal] load metadata for mcr.microsoft.com/dotnet/core/sdk:3.1                                             0.7s
#  => [build 1/7] FROM mcr.microsoft.com/dotnet/core/sdk:3.1@sha256:d09eefeaad2129f0a0ac047095792afc6792e7aae4b8bb1  0.0s
#  => [internal] load build context                                                                                  0.0s
#  => => transferring context: 31.78kB                                                                               0.0s
#  => CACHED [build 2/7] WORKDIR /usr/src                                                                            0.0s
#  => CACHED [runtime 1/3] FROM mcr.microsoft.com/dotnet/core/aspnet:3.1@sha256:4030ec40f9b5c1e8cac5d550639b7b05d1d  0.0s
#  => => resolve mcr.microsoft.com/dotnet/core/aspnet:3.1@sha256:4030ec40f9b5c1e8cac5d550639b7b05d1d6af0c89b4b47d66  0.0s
#  => [runtime 2/3] WORKDIR /usr/src                                                                                 0.0s
#  => [build 3/7] COPY *.csproj .                                                                                    0.0s
#  => ERROR [build 4/7] RUN dotnet restore                                                                           6.3s
# ------
#  > [build 4/7] RUN dotnet restore:
# #11 0.735   Determining projects to restore...
# #11 5.746 /usr/src/SimpleShort.csproj : error NU1101: Unable to find package Compute. No packages exist with this id in source(s): nuget.org
# #11 5.948   Failed to restore /usr/src/SimpleShort.csproj (in 5.01 sec).
# ------
# failed to solve with frontend dockerfile.v0: failed to build LLB: executor failed running [/bin/sh -c dotnet restore]: runc did not terminate sucessfully

# Offical dotnet image as the build environment
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

# DIR: /usr/src
WORKDIR /usr/src

# Copy only the csproj to the current dir
COPY *.csproj .

# Dotnet restore the proj
RUN dotnet restore

# Copy the rest of the files into the dir
COPY . .

# Dotnet publish with Release config outputted to /out
RUN dotnet publish -c Release -o out

# DIR: /usr/src/out
WORKDIR /out

# Offical dotnet image as the runtime environment
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime

# DIR: /usr/src
WORKDIR /usr/src

# Copy the files from the build image into the runtime image
COPY --from=build /app/out .

# Run the application
# ENTRYPOINT ["dotnet", "aspnetapp.dll"]