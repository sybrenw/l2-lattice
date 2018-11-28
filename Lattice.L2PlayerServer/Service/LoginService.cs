using Lattice.L2PlayerServer.Network;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lattice.L2PlayerServer.Service
{
    internal class LoginService
    {
        private static LoginService _instance;

        public static LoginService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LoginService();

                return _instance;
            }
        }

        private LoginServerConnection _connection;

        private LoginService()
        {

        }

        public async Task ConnectAsync(string ip, int port)
        {
            if (_connection == null)
            {
                _connection = new LoginServerConnection(Server.ServerId);
                await _connection.ConnectAsync(ip, port);
            }
        }

        public async Task<bool> GetPlayerAuthedAsync(int accountId, int loginAuthKey, int gameAuthKey)
        {
            if (_connection != null)
                return await _connection.GetPlayerAuthedAsync(accountId, loginAuthKey, gameAuthKey);

           return false;
        }
    }
}
