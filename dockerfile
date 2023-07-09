# Use the official ASP.NET Core runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MessageApp.csproj", ""]
RUN dotnet restore "./MessageApp.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "MessageApp.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "MessageApp.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessageApp.dll"]