FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Kdz.Gateway/Kdz.Gateway.csproj", "Kdz.Gateway/"]
RUN dotnet restore "Kdz.Gateway/Kdz.Gateway.csproj"
COPY . .
WORKDIR "/src/Kdz.Gateway"
RUN dotnet build "Kdz.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kdz.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kdz.Gateway.dll"]
