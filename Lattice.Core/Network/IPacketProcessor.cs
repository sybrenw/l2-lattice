using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lattice.Core.Network
{
    public interface IPacketProcessor
    {
        void RegisterOpcodes();

        Task ProcessAsync(byte[] raw, int offset, int length);


    }
}
