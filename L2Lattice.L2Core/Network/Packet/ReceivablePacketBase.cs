using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L2Lattice.L2Core.Network.Packet
{
    public abstract class ReceivablePacketBase<T> : IReceivablePacket where T : NetworkClient
    {     
        public T Client { get; private set; }
        
        public void Read(T client, byte[] raw)
        {
            Client = client;
            using (MemoryStream stream = new MemoryStream(raw))
            using (BinaryReader reader = new BinaryReader(stream, Encoding.Unicode))
            {
                // Skip opcode
                reader.ReadByte();
                Read(reader);
            }
        }

        public abstract void Read(BinaryReader reader);

        public string ReadString(BinaryReader reader)
        {
            char ch;
            StringBuilder sb = new StringBuilder();
            while ((ch = reader.ReadChar()) != 0)
                sb.Append(ch);

            return sb.ToString();
        }
    }
}
