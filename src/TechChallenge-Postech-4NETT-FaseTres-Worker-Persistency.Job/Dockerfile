FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5008
EXPOSE 5679 # Porta do Prometheus 

ENV ASPNETCORE_URLS=http://+:5008

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/TechChallenge-Postech-4NETT-FaseTres-Worker-Persistency.Job/Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Job.csproj", "src/TechChallenge-Postech-4NETT-FaseTres-Worker-Persistency.Job/"]
RUN dotnet restore "src/TechChallenge-Postech-4NETT-FaseTres-Worker-Persistency.Job/Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Job.csproj"
COPY . .
WORKDIR "/src/src/TechChallenge-Postech-4NETT-FaseTres-Worker-Persistency.Job"
RUN dotnet build "Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Job.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Job.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM build AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Job.dll"]