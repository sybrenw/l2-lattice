using Lattice.Common.QuadTree;
using Lattice.L2Common.Interfaces;
using Lattice.L2Common.Model;
using Lattice.L2PlayerServer.Network;
using Lattice.L2PlayerServer.Network.GamePacket.Server;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Lattice.L2PlayerServer.World
{
    public class L2Region : QuadTreeNode<L2Object>
    {
        public static readonly Vector3 ViewVector = new Vector3(1000.0f, 1000.0f, 0);

        public L2Region[] Children { get; protected set; }
        public ConcurrentDictionary<int, L2Object> Objects { get; } = new ConcurrentDictionary<int, L2Object>();

        private object _modifyLock = new object();

        public Vector3 MinView { get; }
        public Vector3 MaxView { get; }

        public L2Region(int level, Vector3 min, Vector3 max) : base(level, min, max)
        {
            MinView = min - ViewVector;
            MaxView = max + ViewVector;
        }

        public override void Insert(Vector3 position, L2Object obj)
        {
            lock (_modifyLock)
            {
                if (Children == null)
                {
                    Objects.TryAdd(obj.ObjectId, obj);
                    obj.PositionChanged -= Obj_PositionChanged;
                    obj.PositionChanged += Obj_PositionChanged;
                    return;
                }

                int idx = (position.X < Center.X) ? 0 : 2;
                idx += (position.Y < Center.Y) ? 0 : 1;
                Children[idx].Insert(position, obj);
            }
        }

        public override void Remove(L2Object obj)
        {
            lock (_modifyLock)
            {
                Objects.TryRemove(obj.ObjectId, out L2Object o);
                obj.PositionChanged -= Obj_PositionChanged;
            }
        }

        public bool AutoSplitMerge(int minCount, int maxCount)
        {
            if (Children != null)
            {
                bool merge = true;
                foreach (var child in Children)
                {
                    merge &= child.AutoSplitMerge(minCount, maxCount);
                }

                if (merge)
                    Merge();
            }

            if (Children == null)
            {
                if (Objects.Count < minCount)
                    return true;

                if (Objects.Count > maxCount)
                {
                    Split();
                    foreach (var child in Children)
                    {
                        child.AutoSplitMerge(minCount, maxCount);
                    }
                    return false;
                }
            }

            return false;
        }

        private void Obj_PositionChanged(object sender, Vector3 pos, Vector3 old)
        {
            if (pos.X < Min.X || pos.Y < Min.Y || pos.X > Max.X || pos.Y > Max.Y)
            {
                Remove(sender as L2Object);
                L2World.Instance.TransferObject(sender as Character, pos, old);
            }
        }

        public void Broadcast(Vector3 position, ISendableMessage msg)
        {
            if (!InViewRange(position))
                return;

            if (Children == null)
            {
                foreach (L2Object obj in Objects.Values)
                {
                    if (!(obj is Character))
                        continue;

                    Character character = obj as Character;

                    if (character.Controller == null)
                        continue;

                    character.Controller.Receive(msg);
                }
            }
            else
            {
                foreach (var child in Children)
                    child.Broadcast(position, msg);
            }
        }

        public void InformAdded(Vector3 position, Character character, ISendableMessage msg)
        {
            if (!InViewRange(position))
                return;

            if (Children == null)
            {
                foreach (L2Object obj in Objects.Values)
                {
                    if (obj == character)
                        continue;

                    if (obj is Character)
                    {
                        Character c = obj as Character;
                        character.Controller.Receive(new S_0x31_CharInfo(c).WriteSilent());

                        if (c.Controller is GameClient)
                            c.Controller.Receive(msg);

                    }
                }
            }
            else
            {
                foreach (var child in Children)
                    child.InformAdded(position, character, msg);
            }

        }

        public void InformMoved(Vector3 pos, Vector3 oldPos, Character character, ISendableMessage msgAdded, ISendableMessage msgDeleted)
        {
            bool newPosInRange = InViewRange(pos);
            bool oldPosInRange = InViewRange(oldPos);

            if (!oldPosInRange && !newPosInRange)
                return;

            if (Children == null)
            {                
                if (newPosInRange && !oldPosInRange)
                {
                    // Object came into viewrange
                    foreach (L2Object obj in Objects.Values)
                    {
                        if (obj == character)
                            continue;

                        if (obj is Character)
                        {
                            Character c = obj as Character;
                            character.Controller.Receive(new S_0x31_CharInfo(c).WriteSilent());

                            if (c.Controller is GameClient)
                                c.Controller.Receive(msgAdded);

                        }
                    }
                }
                else if (!newPosInRange && oldPosInRange)
                {
                    // Object went out of viewrange
                    foreach (L2Object obj in Objects.Values)
                    {
                        if (obj == character)
                            continue;

                        if (obj is Character)
                        {
                            Character c = obj as Character;
                            character.Controller.Receive(new S_0x08_DeleteObject(c.ObjectId).WriteSilent());

                            if (c.Controller is GameClient)
                                c.Controller.Receive(msgDeleted);

                        }
                    }
                }                                
            }
            else
            {
                foreach (var child in Children)
                    child.InformMoved(pos, oldPos, character, msgAdded, msgDeleted);
            }            
        }

        public override void Split()
        {
            lock (_modifyLock)
            {
                Children = new L2Region[]
                {
                 new L2Region(Level + 1, Min, Center),
                 new L2Region(Level + 1, new Vector3(Min.X, Center.Y, 0), new Vector3(Center.X, Max.Y, 0)),
                 new L2Region(Level + 1, new Vector3(Center.X, Min.Y, 0), new Vector3(Max.X, Center.Y, 0)),
                 new L2Region(Level + 1, Center, Max)
                };

                // Move objects
                foreach(var obj in Objects.Values)
                {
                    Vector3 position = obj.Position;
                    int idx = (position.X < Center.X) ? 0 : 2;
                    idx += (position.Y < Center.Y) ? 0 : 1;
                    Children[idx].Insert(position, obj);
                }
                Objects.Clear();
            }
        }

        public override void Merge()
        {
            lock (_modifyLock)
            {
                foreach (L2Region region in Children)
                {
                    foreach (var obj in region.Objects)
                    {
                        obj.Value.PositionChanged -= region.Obj_PositionChanged;
                        Objects.TryAdd(obj.Key, obj.Value);
                    }
                }
                Children = null;
            }
        }

        private bool InViewRange(Vector3 p)
        {
            if (p.X < MinView.X || p.Y < MinView.Y || p.X > MaxView.X || p.Y > MaxView.Y)
                return false;

            return true;
        }
    }
}
