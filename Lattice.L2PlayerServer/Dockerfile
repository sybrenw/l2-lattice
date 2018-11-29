FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Lattice.L2PlayerServer/Lattice.L2PlayerServer.csproj Lattice.L2PlayerServer/
COPY Lattice.Common/Lattice.Common.csproj Lattice.Common/
COPY Lattice.L2Common/Lattice.L2Common.csproj Lattice.L2Common/
COPY Lattice.L2Core/Lattice.L2Core.csproj Lattice.L2Core/
COPY Lattice.Core/Lattice.Core.csproj Lattice.Core/
RUN dotnet restore Lattice.L2PlayerServer/Lattice.L2PlayerServer.csproj
COPY . .
WORKDIR /src/Lattice.L2PlayerServer
RUN dotnet build Lattice.L2PlayerServer.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Lattice.L2PlayerServer.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
EXPOSE 7777
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Lattice.L2PlayerServer.dll"]
