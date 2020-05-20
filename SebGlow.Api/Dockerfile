FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["SebGlow.Api/SebGlow.Api.csproj", "SebGlow.Api/"]
RUN dotnet restore "SebGlow.Api/SebGlow.Api.csproj"
COPY . .
WORKDIR "/src/SebGlow.Api"
RUN dotnet build "SebGlow.Api.csproj" -c Debug -o /app

FROM build AS publish
RUN dotnet publish "SebGlow.Api.csproj" -c Debug -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SebGlow.Api.dll"]