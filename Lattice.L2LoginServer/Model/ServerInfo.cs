using System;
using System.Collections.Generic;
using System.Text;

namespace Lattice.LoginServer.Model
{
    internal class ServerInfo
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
