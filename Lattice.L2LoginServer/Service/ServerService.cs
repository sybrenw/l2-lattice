using Lattice.L2Core.IdFactory;
using Lattice.L2Core.Object;
using Lattice.LoginServer.Model;
using Lattice.LoginServer.Network;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Lattice.LoginServer.Service
{

    internal class ServerService
    {
        private static ServerService _instance;

        public static ServerService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ServerService();

                return _instance;
            }
        }


        private ServerService()
        {

        }

        private ConcurrentDictionary<int, PlayerServerConnection> _servers = new ConcurrentDictionary<int, PlayerServerConnection>();

        public bool RegisterServer(int serverId, PlayerServerConnection server)
        {
            return _servers.TryAdd(serverId, server);
        }
        
        public void UnregisterServer(PlayerServerConnection server)
        {
            if (_servers.TryGetValue(server.ServerId, out PlayerServerConnection s) && s == server)
                _servers.TryRemove(server.ServerId, out s);
        }

        public List<ServerInfo> GetServerList()
        {
            List<ServerInfo> infoList = new List<ServerInfo>();

            foreach(PlayerServerConnection server in _servers.Values)
            {
                ServerInfo info = new ServerInfo();
                info.Ip = "127.0.0.1";
                info.Port = 7777;
                info.Id = (byte)server.ServerId;
                info.AgeLimit = 0;
                info.Brackets = 1;
                info.PvP = 1;
                info.Status = 1;
                info.MaxPlayers = 9999;
                infoList.Add(info);
            }

            return infoList;
        }
    }
}
