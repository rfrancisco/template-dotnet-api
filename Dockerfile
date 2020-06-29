# ----------------------------------------------------------------
# 1st stage: build component
# ----------------------------------------------------------------

# base image
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS builder
WORKDIR /app

# copy csproj and restore as distinct layers
COPY ./src/*.csproj ./src/
RUN dotnet restore ./src/*.csproj

# copy everything else and build
COPY ./src/ ./src/
RUN dotnet publish ./src/*.csproj -c Release -o /app/dist

# ----------------------------------------------------------------
# 2nd stage: build production ready image
# ----------------------------------------------------------------

# base image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
WORKDIR /app

# expose the environment variable that dotnet uses to determine the active profile. Possible values are "", "Development", "Staging", "Production"
ENV ASPNETCORE_ENVIRONMENT ""

# copy artifact build from the 'build environment'
COPY --from=builder /app/dist .
COPY /scripts/entrypoint.sh /app/

# expose http port
EXPOSE 5000
# expose https port
EXPOSE 5001

ENTRYPOINT ["dotnet", "dotnet projectAssemblyName.api.dll"]
