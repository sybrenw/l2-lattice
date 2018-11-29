FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Lattice.L2LoginServer/Lattice.L2LoginServer.csproj Lattice.L2LoginServer/
COPY Lattice.Common/Lattice.Common.csproj Lattice.Common/
COPY Lattice.L2Core/Lattice.L2Core.csproj Lattice.L2Core/
COPY Lattice.Core/Lattice.Core.csproj Lattice.Core/
COPY Lattice.L2Common/Lattice.L2Common.csproj Lattice.L2Common/
RUN dotnet restore Lattice.L2LoginServer/Lattice.L2LoginServer.csproj
COPY . .
WORKDIR /src/Lattice.L2LoginServer
RUN dotnet build Lattice.L2LoginServer.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Lattice.L2LoginServer.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
EXPOSE 2106 2110
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Lattice.L2LoginServer.dll"]
