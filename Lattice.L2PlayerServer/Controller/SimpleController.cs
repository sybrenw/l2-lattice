using Lattice.L2Common.Interfaces;
using Lattice.L2Common.Model;
using Lattice.L2PlayerServer.Network.GamePacket.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lattice.L2PlayerServer.Controller
{
    public class SimpleController : IController
    {
        private static readonly Random rnd = new Random();
        public Character Character { get; }

        private long _lastUpdate = 0;

        public SimpleController(Character character)
        {
            Character = character;
            character.Controller = this;
        }

        public void Receive(ISendableMessage message)
        {
            // Ignore
        }

        public void Update(long milis)
        {
            if (milis - _lastUpdate < 1000)
                return;
            
            double prob = rnd.NextDouble();
            if (prob < 0.94)
                return;
            else if (prob < 0.97)
            {
                int[] pos = new int[] { (int)Character.Position.X, (int)Character.Position.Y, (int)Character.Position.Z };
                int[] dest = new int[] { (int)Character.Position.X + rnd.Next(-100, 100), (int)Character.Position.Y + rnd.Next(-100, 100), (int)Character.Position.Z };
                Character.Position = new System.Numerics.Vector3(dest[0], dest[1], dest[2]);
                Character.Broadcast(new S_0x2F_MoveToLocation(Character.ObjectId, dest, pos).WriteSilent());
            }
            else
            {
                Character.Broadcast(new S_0x27_SocialAction(Character.ObjectId, rnd.Next(1,40)).WriteSilent());
            }
        }
    }
}
