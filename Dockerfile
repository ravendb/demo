FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

WORKDIR /app

RUN curl -sL https://deb.nodesource.com/setup_18.x | bash - \
    && apt-get update \
    && apt-get install -y nodejs 

COPY . ./
RUN dotnet restore \ 
    && cd DemoServer \
    && dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV RAVENDEMO_Database__Urls__0="http://live-test.ravendb.net" RAVENDEMO_Database__Name="Demo" RAVENDEMO_ConferenceMode=false RAVENDEMO_GoogleTagManager__ContainerId=""
EXPOSE 80
WORKDIR /app
COPY --from=build-env /app/DemoServer/out .
ENTRYPOINT ["dotnet", "DemoServer.dll"]
