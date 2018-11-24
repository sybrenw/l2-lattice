using System;
using System.Collections.Generic;
using System.Text;

namespace L2Lattice.LoginServer.Enum
{
    internal enum LoginState
    {
        Connected,
        Authed,
        LoginOk,
        LoginFail,
        Banned,
        PlayOk,
        PlayFail
    }
}
