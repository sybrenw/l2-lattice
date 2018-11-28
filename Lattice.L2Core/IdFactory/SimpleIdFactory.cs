using System;
using System.Collections.Generic;
using System.Text;

namespace Lattice.L2Core.IdFactory
{
    public class SimpleIdFactory
    {
        private int _current = 0;

        public int Create()
        {
            int id = ++_current;
            return id;
        }
    }
}
