using System;
using System.Collections.Generic;
using System.Text;

namespace Lattice.L2Common.Interfaces
{
    public interface INotifyObject
    {
        event EventHandler<ISendableMessage> Broadcasting;
        void Broadcast(ISendableMessage obj);
    }
}
