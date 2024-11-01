FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5008
EXPOSE 5679

ENV ASPNETCORE_URLS=http://+:5008

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src

COPY ["src/TechChallenge-Postech-4NETT-FaseTres-Worker-Persistency.Job/Postech.TechChallenge.Persistency.Job.csproj", "src/TechChallenge-Postech-4NETT-FaseTres-Worker-Persistency.Job/"]
COPY ["src/TechChallenge-Postech-4NETT-FaseTres-Worker-Persistency.Application/Postech.TechChallenge.Persistency.Application.csproj", "src/TechChallenge-Postech-4NETT-FaseTres-Worker-Persistency.Application/"]
COPY ["src/TechChallenge-Postech-4NETT-FaseTres-Worker-Persistency.Core/Postech.TechChallenge.Persistency.Core.csproj", "src/TechChallenge-Postech-4NETT-FaseTres-Worker-Persistency.Core/"]
COPY ["src/TechChallenge-Postech-4NETT-FaseTres-Worker-Persistency.Infrastructure/Postech.TechChallenge.Persistency.Infra.csproj", "src/TechChallenge-Postech-4NETT-FaseTres-Worker-Persistency.Infrastructure/"]

RUN dotnet restore "src/TechChallenge-Postech-4NETT-FaseTres-Worker-Persistency.Job/Postech.TechChallenge.Persistency.Job.csproj"

COPY . .

RUN dotnet build "src/TechChallenge-Postech-4NETT-FaseTres-Worker-Persistency.Job/Postech.TechChallenge.Persistency.Job.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "src/TechChallenge-Postech-4NETT-FaseTres-Worker-Persistency.Job/Postech.TechChallenge.Persistency.Job.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Postech.TechChallenge.Persistency.Job.dll"]
