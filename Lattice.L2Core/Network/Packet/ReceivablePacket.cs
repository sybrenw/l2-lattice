using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Lattice.L2Core.Network.Packet
{
    public abstract class ReceivablePacket<T> : IReceivablePacket<T> where T : L2Client
    {     
        public T Client { get; private set; }
        
        public async Task ReadAsync(T client, byte[] raw)
        {
            Client = client;
            using (MemoryStream stream = new MemoryStream(raw))
            using (BinaryReader reader = new BinaryReader(stream, Encoding.Unicode))
            {
                // Skip opcode
                reader.ReadByte();
                await ReadAsync(reader);
            }
        }

        protected virtual void Read(BinaryReader reader) { }

        protected virtual async Task ReadAsync(BinaryReader reader)
        {
            Read(reader);
            await Task.CompletedTask;
        }

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
