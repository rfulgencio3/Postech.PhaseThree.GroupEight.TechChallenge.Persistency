@echo off

:: Verifica se a rede externa existe
docker network ls | findstr "techchallenge-worker-persistency_custom_network" > nul
if %errorlevel% neq 0 (
    echo Criando a rede externa techchallenge-worker-persistency_custom_network...
    docker network create --subnet=192.168.80.0/24 techchallenge-postech-4nett-fasetres-worker-persistency_custom_network

) else (
    echo Rede techchallenge-worker-persistency_custom_network ja existe.
)

:: Executa o Docker Compose
docker-compose up -d --build