#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Photosoil.API/Photosoil.API.csproj", "Photosoil.API/"]
RUN dotnet restore "Photosoil.API/Photosoil.API.csproj"
COPY . .
WORKDIR "/src/Photosoil.API"
RUN dotnet build "Photosoil.API.csproj" -c Release -o /app/build
#SHELL ["/bin/bash", "-c", "chmod +x /Storage/"] 

FROM build AS publish
RUN dotnet publish "Photosoil.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#RUN chmod -R 777 /app

ENTRYPOINT ["dotnet", "Photosoil.API.dll"]	