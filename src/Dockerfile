FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY *.sln .
COPY Demergenza/Demergenza.csproj ./Demergenza/
COPY Demergenza.Persistence/Demergenza.Persistence.csproj ./Demergenza.Persistence/
COPY Demergenza.Domain/Demergenza.Domain.csproj ./Demergenza.Domain/
COPY Demergenza.Application/Demergenza.Application.csproj ./Demergenza.Application/
COPY Demergenza.Infrastructure/Demergenza.Infrastructure.csproj ./Demergenza.Infrastructure/

RUN dotnet restore

COPY Demergenza/. ./Demergenza/
COPY Demergenza.Domain/. ./Demergenza.Domain/
COPY Demergenza.Application/. ./Demergenza.Application/
COPY Demergenza.Persistence/. ./Demergenza.Persistence/
COPY Demergenza.Infrastructure/. ./Demergenza.Infrastructure/
RUN dotnet publish Demergenza/Demergenza.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

RUN mkdir configuration
RUN mkdir -p wwwroot/data-images
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "Demergenza.dll"]
