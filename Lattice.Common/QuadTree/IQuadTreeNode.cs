using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Lattice.Common.QuadTree
{
    public interface IQuadTreeNode<T>
    {
        int Level { get; }
        Vector3 Min { get; }
        Vector3 Max { get; }

        // Add / remove objects
        void Insert(Vector3 position, T obj);
        void Remove(T obj);
               
        void Split();
        void Merge();
    }
}
