using Lattice.L2Common.Model;
using Lattice.L2PlayerServer.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Lattice.L2PlayerServer.World
{
    public class L2World
    {
        private static L2World _instance;

        public static L2World Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new L2World();

                return _instance;
            }
        }

        public const int MIN_X = -294912;
        public const int MAX_X = 196608;
        public const int MIN_Y = -262144;
        public const int MAX_Y = 262144;

        public const int DEFAULT_TILE_SIZE = 32768;

        private ConcurrentBag<Player> _players = new ConcurrentBag<Player>();
        private ConcurrentBag<Character> _characters = new ConcurrentBag<Character>();

        internal void InsertPlayer(Player player)
        {
            _players.Add(player);
        }

        public void Broadcast(byte[] raw, int rawLength, Vector3 origin, int radius)
        {
            float radiusSq = radius * radius;
            foreach(Player player in _players)
            {
                if (player.Character == null || player.Client == null)
                    continue;

                if ((player.Character.Position - origin).LengthSquared() > radiusSq)
                    continue;

                player.Client.SendPacketAsync(raw, rawLength);
            }
        }

    }
}
