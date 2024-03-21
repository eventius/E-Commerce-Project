#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["E-Commmerce-Project/E-Commmerce-Project.csproj", "E-Commmerce-Project/"]
RUN dotnet restore "E-Commmerce-Project/E-Commmerce-Project.csproj"
COPY . .
WORKDIR "/src/E-Commmerce-Project"
RUN dotnet build "E-Commmerce-Project.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "E-Commmerce-Project.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "E-Commmerce-Project.dll"]