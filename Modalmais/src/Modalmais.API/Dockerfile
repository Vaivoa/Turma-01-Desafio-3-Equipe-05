#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Modalmais.API/Modalmais.API.csproj", "src/Modalmais.API/"]
COPY ["src/Modalmais.Infra/Modalmais.Infra.csproj", "src/Modalmais.Infra/"]
COPY ["src/Modalmais.Business/Modalmais.Business.csproj", "src/Modalmais.Business/"]
RUN dotnet restore "src/Modalmais.API/Modalmais.API.csproj"
COPY . .
WORKDIR "/src/src/Modalmais.API"
RUN dotnet build "Modalmais.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Modalmais.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Modalmais.API.dll"]
