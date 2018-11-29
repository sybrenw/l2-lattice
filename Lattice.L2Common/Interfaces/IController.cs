using System;
using System.Collections.Generic;
using System.Text;

namespace Lattice.L2Common.Interfaces
{
    public interface IController
    {
        void Receive(ISendableMessage message);
    }
}
