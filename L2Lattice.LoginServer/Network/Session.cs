using L2Lattice.LoginServer.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace L2Lattice.LoginServer.Network
{
    public class Session
    {
        public int Id { get; }
        public LoginState State { get; set; } = LoginState.Connected;

        public int AccountId { get; set; }
        public int LoginAuthKey { get; set; }
        public int GameAuthKey { get; set; }
        
        public Session(int id)
        {
            Id = id;
        }
        
        public bool Verify(int accountId, int authKey)
        {
            return accountId == AccountId && authKey == LoginAuthKey;
        }
    }
}
