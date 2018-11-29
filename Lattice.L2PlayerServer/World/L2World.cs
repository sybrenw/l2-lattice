using Lattice.L2Common.Enum;
using Lattice.L2Common.Interfaces;
using Lattice.L2Common.Model;
using Lattice.L2Common.World;
using Lattice.L2PlayerServer.Controller;
using Lattice.L2PlayerServer.Model;
using Lattice.L2PlayerServer.Network;
using Lattice.L2PlayerServer.Network.GamePacket.Server;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lattice.L2PlayerServer.World
{
    public class L2World : IRegion
    {
        private static L2World _instance;

        public static L2World Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new L2World();

                return _instance;
            }
        }


        public const int MIN_X = -294912;
        public const int MAX_X = 196608;
        public const int MIN_Y = -262144;
        public const int MAX_Y = 262144;

        public const int DEFAULT_TILE_SIZE = 32768;

        public int ObjectCount => _objects.Count;

        private ConcurrentDictionary<int, L2Object> _objects = new ConcurrentDictionary<int, L2Object>();

        private List<SimpleController> _controllers = new List<SimpleController>();

        private L2World()
        {
            for (int i = 0; i < 200; i++)
            {
                int ix = i % 40;
                int iy = (i - ix) / 40;

                Character c = new Character();
                c.Name = "Kerzouner";
                c.ObjectId = 100 + i;
                c.Race = 0;
                c.Sex = 0;
                c.VisibleClassId = 90;
                c.Position = new Vector3(145980 + (ix - 20) * 15, 13448 - iy * 15, -1151);
                AddObject(c);
                _controllers.Add(new SimpleController(c));
            }
                       
            Task.Run(async () => {
                await Task.Delay(10000);
                Random rnd = new Random();
                while (true)
                {
                    long milis = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                    foreach(SimpleController controller in _controllers)
                    {
                        controller.Update(milis);
                    }
                    await Task.Delay(100);
                }
            });
        }

        public void AddObject(L2Object obj)
        {
            if (_objects.TryAdd(obj.ObjectId, obj))
            {
                if (obj is INotifyObject)
                {
                    (obj as INotifyObject).Broadcasting += L2World_Broadcasting;

                    if (obj is Character && (obj as Character).Controller is GameClient)
                        InformObject(obj as Character);
                }
            }
            else
            {
                // Failed
            }
        }

        public void RemoveObject(L2Object obj)
        {
            L2Object obj2;
            if (_objects.TryRemove(obj.ObjectId, out obj2))
            {
                if (obj is INotifyObject)
                {
                    (obj as INotifyObject).Broadcasting -= L2World_Broadcasting;
                }
            }
        }

        private void InformObject(Character character)
        {
            Task.Run(async () =>
            {
                await Task.Delay(5000);
                var packet = new S_0x31_CharInfo(character).WriteSilent();
                foreach (L2Object obj in _objects.Values)
                {
                    if (obj == character)
                        continue;

                    if (obj is Character)
                    {
                        Character c = obj as Character;
                        character.Controller.Receive(new S_0x31_CharInfo(c).WriteSilent());

                        if (c.Controller is GameClient)
                            c.Controller.Receive(packet);
                        
                    }
                }
            });
        }

        private void L2World_Broadcasting(object sender, ISendableMessage e)
        {
            foreach (L2Object obj in _objects.Values)
            {
                if (!(obj is Character))
                    continue;

                Character character = obj as Character;

                if (character.Controller == null)
                    continue;

                character.Controller.Receive(e);
            }
        }

        public void Say(L2Object obj, ChatType type, string text, string target)
        {
            throw new NotImplementedException();
        }

        public void ExecuteAction(L2Object obj, object action)
        {
            throw new NotImplementedException();
        }
    }
}
