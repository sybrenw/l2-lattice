using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lattice.Core.Network
{
    public class PacketProcessor : IPacketProcessor
    {
        private byte[] _opcodes;

        public void RegisterOpcodes()
        {
            throw new NotImplementedException();
        }

        public Task ProcessAsync(byte[] raw, int offset, int length)
        {
            throw new NotImplementedException();
        }
    }
}
