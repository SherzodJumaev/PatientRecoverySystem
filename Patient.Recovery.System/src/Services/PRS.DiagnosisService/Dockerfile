FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["src/Services/PRS.DiagnosisService/PRS.DiagnosisService.csproj", "src/Services/PRS.DiagnosisService/"]
RUN dotnet restore "src/Services/PRS.DiagnosisService/PRS.DiagnosisService.csproj"
COPY . .

WORKDIR /src/src/Services/PRS.DiagnosisService

RUN dotnet build "PRS.DiagnosisService.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PRS.DiagnosisService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PRS.DiagnosisService.dll"]