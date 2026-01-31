# STAGE 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# STAGE 2: Final Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy AS final
WORKDIR /app
COPY --from=build /app/publish .

# Install the missing "Culture" libraries to make SQL happy
RUN apt-get update && apt-get install -y libicu-dev

# Ensure we are NOT in Invariant mode
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

EXPOSE 8080
ENTRYPOINT ["dotnet", "Legacysql.dll"]