#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Compact.Services.Api/Compact.Services.Api.csproj", "Compact.Services.Api/"]
RUN dotnet restore "Compact.Services.Api/Compact.Services.Api.csproj"
COPY . .
WORKDIR "/src/Compact.Services.Api"
RUN dotnet build "Compact.Services.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Compact.Services.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Compact.Services.Api.dll"]