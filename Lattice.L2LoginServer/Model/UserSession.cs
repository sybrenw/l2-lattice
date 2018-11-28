using System;
using System.Collections.Generic;
using System.Text;

namespace Lattice.LoginServer.Model
{
    internal class UserSession
    {
        public int AuthKeyLogin { get; set; }
        public int AuthKeyGame { get; set; }
        public int AccountId => _account.AccountId;
        
        private Account _account;

        public UserSession()
        {

        }
        

    }
}
