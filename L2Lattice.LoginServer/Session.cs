using System;
using System.Collections.Generic;
using System.Text;

namespace L2Lattice.LoginServer
{
    public class Session
    {
        public int Id { get; }
        public int AccountId { get; set; }
        public int AuthKey { get; set; }

        public Session(int id)
        {
            Id = id;
        }
        
        public bool Verify(int accountId, int authKey)
        {
            return accountId == AccountId && authKey == AuthKey;
        }
    }
}
