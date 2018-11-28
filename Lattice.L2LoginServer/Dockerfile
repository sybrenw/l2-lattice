FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY L2Lattice.LoginServer/L2Lattice.LoginServer.csproj L2Lattice.LoginServer/
COPY L2Lattice.L2Core/L2Lattice.L2Core.csproj L2Lattice.L2Core/
COPY L2Lattice.Common/L2Lattice.Common.csproj L2Lattice.Common/
RUN dotnet restore L2Lattice.LoginServer/L2Lattice.LoginServer.csproj
COPY . .
WORKDIR /src/L2Lattice.LoginServer
RUN dotnet build L2Lattice.LoginServer.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish L2Lattice.LoginServer.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "L2Lattice.LoginServer.dll"]
