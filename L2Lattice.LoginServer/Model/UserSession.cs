using System;
using System.Collections.Generic;
using System.Text;

namespace L2Lattice.LoginServer.Model
{
    internal class UserSession
    {
        public int AuthKeyLogin { get; set; }
        public int AuthKeyGame { get; set; }
        public int AccountId => _account.Id;
        
        private Account _account;

        public UserSession()
        {

        }
        

    }
}
