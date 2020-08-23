# Description
L2 lattice is a Lineage 2 server (Grand Crusade) framework to distribute world load over multiple servers. It is written .NET Core 2.1

I wrote this mostly for fun to play around with .NET Core. 

The server does work but does not implement any of the game's features, but it is functional to the point that you can login and run around. 

Some ideas I experimented with in this project:
- Split the world up in a grid and distribute the load over several servers. 
- Update/restart the world/gameserver without the players disconnecting. 
- Load 100.000 players
