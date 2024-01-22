FROM mcr.microsoft.com/dotnet/aspnet:8.0

ARG ARG_AES_KEY=""
ENV AES_KEY=$ARG_AES_KEY

WORKDIR /app
COPY ./publish/cs-hello-world /app/publish/cs-hello-world
ENTRYPOINT ["dotnet", "cs-hello-world"]
EXPOSE 8080
