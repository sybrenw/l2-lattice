using L2Lattice.PlayerServer.Network;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace L2Lattice.PlayerServer.Service
{
    public class PlayerService
    {
        private static PlayerService _instance;

        public static PlayerService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PlayerService();

                return _instance;
            }
        }
                       
        private PlayerService()
        {

        }

    }
}
