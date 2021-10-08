#base image
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS Build

WORKDIR /app

COPY  *.sln ./
COPY StoreBL/*.csproj ./StoreBL/
COPY DL/*.csproj ./DL/
COPY WebUI/*.csproj ./WebUI/
COPY Tests/*.csproj ./Tests/
COPY Models/*.csproj ./Models/

RUN cd WebUI && dotnet restore

COPY . ./

RUN dotnet publish WebUI -c Release -o publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS run

WORKDIR /app

COPY --from=Build /app/publish ./

CMD ["dotnet", "WebUI.dll"]