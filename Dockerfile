FROM mcr.microsoft.com/dotnet/sdk:8.0 AS builder
WORKDIR /src
COPY . .
RUN dotnet restore \
    && dotnet build --no-restore \
    && dotnet publish -c Release -o /src/publish/ /p:UseAppHost=false \
    && cd publish \
    && ls -la

FROM mcr.microsoft.com/dotnet/aspnet:8.0
ARG ARG_CS_HELLO_WORLD_AES_KEY=""
ENV TZ=Asia/Shanghai
ENV CS_HELLO_WORLD_AES_KEY=$ARG_CS_HELLO_WORLD_AES_KEY

WORKDIR /app
#COPY ./publish/ /app/publish/
COPY --from=builder /src/publish/ /app/publish/
RUN ls -ls ./publish/
ENTRYPOINT dotnet ./publish/cs-hello-world.dll
EXPOSE 8080
