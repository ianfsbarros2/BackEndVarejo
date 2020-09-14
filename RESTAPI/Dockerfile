FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY ./Application/*.csproj ./Application/
COPY ./Application.DTO/*.csproj ./Application.DTO/
COPY ./Application.Test/*.csproj ./Application.Test/
COPY ./Domain/*.csproj ./Domain/
COPY ./Domain.Core/*.csproj ./Domain.Core/
COPY ./Domain.Services/*.csproj ./Domain.Services/
COPY ./Infrastructure.Data/*.csproj ./Infrastructure.Data/
COPY ./Infrastruture.CrossCutting.Adapter/*.csproj ./Infrastruture.CrossCutting.Adapter/
COPY ./Infrastruture.CrossCutting.IOC/*.csproj ./Infrastruture.CrossCutting.IOC/
COPY ./Infrastruture.Repository/*.csproj ./Infrastruture.Repository/
COPY ./Presentation/*.csproj ./Presentation/
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Presentation.dll"]