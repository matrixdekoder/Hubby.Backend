FROM microsoft/dotnet:aspnetcore-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:sdk AS build
WORKDIR /src
COPY . .
WORKDIR /src/src/Host.Api
RUN dotnet restore
RUN dotnet build --no-restore -c Release -o /app

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Host.Api.dll"]
