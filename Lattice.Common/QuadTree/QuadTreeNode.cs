using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Lattice.Common.QuadTree
{
    public abstract class QuadTreeNode<T> : IQuadTreeNode<T>
    {
        // Geohash key
        public long Id { get; set; }
        public int Level { get; }
        public Vector3 Min { get; }
        public Vector3 Max { get; }
        public Vector3 Center { get; }

        public QuadTreeNode(int level, Vector3 min, Vector3 max) 
        {
            Level = level;
            Min = min;
            Max = max;
            Center = (Max + Min) / 2;
        }

        public abstract void Insert(Vector3 position, T obj);

        public abstract void Remove(T obj);

        public abstract void Split();

        public abstract void Merge();
    }
}
