using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L2Lattice.L2Core.Network.Packet
{
    public abstract class SendablePacketBase<T> : ISendablePacket where T : NetworkClient
    {
        public T Client { get; private set; }

        private byte _opcode;
        private ushort _opcode2;

        public SendablePacketBase(byte opcode)
        {
            _opcode = opcode;
        }
        
        public SendablePacketBase(byte opcode, ushort opcode2)
        {
            _opcode = opcode;
                       
            _opcode2 = (ushort)((opcode2 & 0xFF00) >> 8);
            _opcode2 |= (ushort)((opcode2 & 0x00FF) << 8);
        }

        public int Write(T client, out byte[] buffer)
        {            
            Client = client;
            int length = 0;
            using (MemoryStream stream = new MemoryStream(new byte[1024]))
            using (BinaryWriter writer = new BinaryWriter(stream, Encoding.Unicode))
            {
                // Reserve header (size)
                writer.Write((short)0);
                // Write opcode
                writer.Write(_opcode);
                if (_opcode2 != 0)
                    writer.Write(_opcode2);
                Write(writer);
                writer.Flush();
                length = (int) stream.Position;
                buffer = stream.ToArray();
            }

            return length;
        }

        public abstract void Write(BinaryWriter writer);

        public void WriteString(BinaryWriter writer, string text)
        {
            writer.Write(Encoding.Unicode.GetBytes(text));
            writer.Write((char)0);
        }
    }
}
