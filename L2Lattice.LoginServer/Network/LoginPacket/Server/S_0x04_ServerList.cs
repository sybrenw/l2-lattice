using L2Lattice.L2Core.Network.Packet;
using L2Lattice.LoginServer.Model;
using L2Lattice.LoginServer.Service;
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
            List<ServerInfo> servers = ServerService.Instance.GetServerList();

            writer.Write((byte)servers.Count);
            writer.Write((byte)1);

            foreach (ServerInfo server in servers)
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
    }
}
