FROM mcr.microsoft.com/dotnet/aspnet:8.0

ARG ARG_CS_HELLO_WORLD_AES_KEY=""
ENV TZ=Asia/Shanghai
ENV CS_HELLO_WORLD_AES_KEY=$ARG_CS_HELLO_WORLD_AES_KEY

WORKDIR /app
COPY ./publish/cs-hello-world /app/publish/cs-hello-world
ENTRYPOINT ["dotnet", "cs-hello-world.dll"]
EXPOSE 8080
