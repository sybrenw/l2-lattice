using Lattice.L2Common.Enum;
using Lattice.L2Common.Interfaces;
using Lattice.L2Common.Model;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Lattice.L2Common.World
{
    public interface IRegion
    {
        int ObjectCount { get; }

        void AddObject(L2Object obj);
        void RemoveObject(L2Object obj);

        void Say(L2Object obj, ChatType type, string text, string target);
        void ExecuteAction(L2Object obj, object action);
    }
}
