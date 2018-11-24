using L2Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace L2Lattice.LoginServer.Network.LoginPacket.Server
{
    internal class S_0x04_ServerList : SendablePacket<LoginClient>
    {
        public static byte Opcode { get; } = 0x04;

        public S_0x04_ServerList() : base(Opcode)
        {

        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((byte)_servers.Count);
            writer.Write((byte)1);

            foreach (ServerDummy server in _servers)
            {
                var ip = IPAddress.Parse(server.Ip);
                writer.Write(server.Id);
                writer.Write(ip.GetAddressBytes());
                writer.Write(server.Port);
                writer.Write(server.AgeLimit);
                writer.Write(server.PvP);
                writer.Write(server.CurrentPlayers);
                writer.Write(server.MaxPlayers);
                writer.Write(server.Status);
                writer.Write(server.Brackets);
                writer.Write(server.ServerType);
            }

            writer.Write((short)164);
            writer.Write((byte)0x01);
            writer.Write((byte)0x04);
            writer.Write((byte)0x15);
            writer.Write(new byte[161]);
        }

        private static List<ServerDummy> _servers = new List<ServerDummy>()
        {
            new ServerDummy() { Id = 1, Ip = "127.0.0.1", Port = 7777, AgeLimit = 0, PvP = 0x01, CurrentPlayers = 0, MaxPlayers = 9999, Status = 0x01, ServerType = 0x00, Brackets = 0x00 }
        };


        private class ServerDummy
        {
            public byte Id { get; set; }
            public string Ip { get; set; }
            public int Port { get; set; }
            public byte AgeLimit { get; set; }
            public byte PvP { get; set; }
            public short CurrentPlayers { get; set; }
            public short MaxPlayers { get; set; }
            public byte Status { get; set; }
            public int ServerType { get; set; }
            public byte Brackets { get; set; }
        }
    }
}
