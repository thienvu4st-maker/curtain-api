# Build v2 with Categories feature
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["media_app_api.csproj", "./"]
RUN dotnet restore "media_app_api.csproj"
COPY . .
RUN dotnet publish "media_app_api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000
ENTRYPOINT ["dotnet", "media_app_api.dll"]
