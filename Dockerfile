# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env

WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY WebDeApplication/*.csproj ./WebDeApplication/
RUN dotnet restore

# copy everything else and build app
COPY WebDeApplication/. ./WebDeApplication/
WORKDIR /source/WebDeApplication
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2

WORKDIR /app
COPY --from=build-env /app ./
ENTRYPOINT ["dotnet", "WebDeApplication.dll"]
