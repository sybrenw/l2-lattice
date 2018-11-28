using Lattice.L2Core.Network;
using Lattice.L2Core.Network.Packet;
using Lattice.L2PlayerServer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lattice.L2PlayerServer.Network.GamePacket.Server
{
    internal class S_0x0D_NewCharacterSuccess : SendablePacket<GameClient>
    {
        public const byte Opcode = 0x0D;

        public S_0x0D_NewCharacterSuccess() : base(Opcode)
        {

        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(_templates.Count);
            foreach(ClassTemplate template in _templates)
            {
                writer.Write(template.Race);
                writer.Write(template.Class);
                writer.Write(1);
                writer.Write(template.Stats.STR);
                writer.Write(99);
                writer.Write(1);
                writer.Write(template.Stats.DEX);
                writer.Write(99);
                writer.Write(1);
                writer.Write(template.Stats.CON);
                writer.Write(99);
                writer.Write(1);
                writer.Write(template.Stats.INT);
                writer.Write(99);
                writer.Write(1);
                writer.Write(template.Stats.WIT);
                writer.Write(99);
                writer.Write(1);
                writer.Write(template.Stats.MEN);
                writer.Write(99);
            }
        }

        private List<ClassTemplate> _templates = new List<ClassTemplate>()
        {
            new ClassTemplate()
            {
                Race = 0,
                Class = 0,
                Stats = Stats.Default
            },
            new ClassTemplate()
            {
                Race = 0,
                Class = 10,
                Stats = Stats.Default
            }
        };
    }
}
