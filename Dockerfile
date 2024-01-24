﻿FROM mcr.microsoft.com/dotnet/sdk:8.0 AS builder
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /src/publish/

FROM mcr.microsoft.com/dotnet/aspnet:8.0
ARG ARG_CS_HELLO_WORLD_AES_KEY=""
ENV TZ=Asia/Shanghai
ENV DOTNET_ENVIRONMENT=prd
ENV DOTNET_HTTP_PORTS=8080
ENV CS_HELLO_WORLD_AES_KEY=$ARG_CS_HELLO_WORLD_AES_KEY

WORKDIR /app
#COPY ./publish/ /app/publish/
COPY --from=builder /src/publish/* /app/
RUN echo $CS_HELLO_WORLD_AES_KEY && echo $DOTNET_ENVIRONMENT
ENTRYPOINT dotnet cs-hello-world.dll
EXPOSE 8080
