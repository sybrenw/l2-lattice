using System;
using System.Collections.Generic;
using System.Text;

namespace Lattice.LoginServer.Enum
{
    internal enum LoginResult : int
    {
        LoginOk = 0,
        LoginFail = -1,
        Banned = -2
    }
}
