using Lattice.L2Common.Interfaces;
using Lattice.L2Common.Model;
using Lattice.L2PlayerServer.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lattice.L2PlayerServer.Model
{
    public class PlayerController : IController
    {
        public GameClient Client { get; set; }

        public void Receive(ISendableMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
