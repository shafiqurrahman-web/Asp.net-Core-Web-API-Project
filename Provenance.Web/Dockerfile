FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Provenance.Web/Provenance.Web.csproj", "Provenance.Web/"]
COPY ["ServiceLayer/ServiceLayer.csproj", "ServiceLayer/"]
COPY ["DataLayer/DataLayer.csproj", "DataLayer/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "Provenance.Web/Provenance.Web.csproj"
COPY . .
WORKDIR "/src/Provenance.Web"
RUN dotnet build "Provenance.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Provenance.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Provenance.Web.dll"]