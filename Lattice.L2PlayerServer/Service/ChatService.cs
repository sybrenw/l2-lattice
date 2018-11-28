using Lattice.L2Common.Enum;
using Lattice.L2Common.Model;
using Lattice.L2PlayerServer.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lattice.L2PlayerServer.Service
{
    public class ChatService
    {
        private static ChatService _instance;

        public static ChatService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ChatService();

                return _instance;
            }
        }

        private ChatService()
        {

        }

        public void Say(Character character, ChatType type, string text, string target)
        {

        }
    }
}
