FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_ENVIRONMENT= Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Whatsapp.API/Whatsapp.API.csproj", "Whatsapp.API/"]
RUN dotnet restore "Whatsapp.API/Whatsapp.API.csproj"
COPY . .
WORKDIR "/src/Whatsapp.API"
RUN dotnet build "Whatsapp.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Whatsapp.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Whatsapp.API.dll"]
