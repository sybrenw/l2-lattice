using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace Lattice.Core.Util
{
    public class BufferReader
    {
        public BufferReader(ReadOnlySequence<byte> seq)
        {
            BitConverter.ToInt16(seq.Slice(0, 2).First.Span);
        }

    }
}
