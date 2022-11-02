#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["chat-api/chat-api.csproj", "chat-api/"]
RUN dotnet restore "chat-api/chat-api.csproj"
COPY . .
WORKDIR "/src/chat-api"
RUN dotnet build "chat-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "chat-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "chat-api.dll"]