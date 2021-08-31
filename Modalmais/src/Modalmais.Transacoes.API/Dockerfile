#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Modalmais.Transacoes.API/Modalmais.Transacoes.API.csproj", "src/Modalmais.Transacoes.API/"]
COPY ["src/Modalmais.Business/Modalmais.Business.csproj", "src/Modalmais.Business/"]
COPY ["src/Modalmais.Core/Modalmais.Core.csproj", "src/Modalmais.Core/"]
RUN dotnet restore "src/Modalmais.Transacoes.API/Modalmais.Transacoes.API.csproj"
COPY . .
WORKDIR "/src/src/Modalmais.Transacoes.API"
RUN dotnet build "Modalmais.Transacoes.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Modalmais.Transacoes.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Modalmais.Transacoes.API.dll"]
