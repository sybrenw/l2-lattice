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
using System.IO;
using System.Numerics;
using System.Text;
using System.Threading;
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

        public const float VISIBLE_RANGE = 1000 * 1000;
        public const int MAX_COUNT = 100;
        public const int MIN_COUNT = 10;

        public const int MIN_X = -294912;
        public const int MAX_X = 196608;
        public const int MIN_Y = -262144;
        public const int MAX_Y = 262144;

        public const int DEFAULT_TILE_SIZE = 32768;

        public int ObjectCount => _objects.Count;

        private ConcurrentDictionary<int, L2Object> _objects = new ConcurrentDictionary<int, L2Object>();

        private List<SimpleController> _controllers = new List<SimpleController>();

        private L2Region RootRegion = new L2Region(0, new Vector3(-294912, -294912, 0), new Vector3(294912, 294912, 0));

        private L2World()
        {
            int countX = 1000;
            int countY = 1000;
            for (int i = 0; i < 10000; i++)
            {
                int ix = i % countX;
                int iy = (i - ix) / countX;

                Character c = new Character();
                c.Name = "Kerzouner";
                c.ObjectId = 100 + i;
                c.Race = 0;
                c.Sex = 0;
                c.VisibleClassId = 90;
                c.Position = new Vector3(145980 + (ix - countX / 2) * 100, 13448 - (iy - countY / 2) * 100, -1151);
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
                    
                    await Task.Delay(300);
                }
            });

            Task.Run(() =>
            {
                while(true)
                {
                    RootRegion.AutoSplitMerge(MIN_COUNT, MAX_COUNT);
                    //Export();
                    Thread.Sleep(10000);
                }
            });
        }

        private void Export()
        {
            int scale = 1;
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            StringBuilder sb = new StringBuilder();
            sb.Append("<svg xmlns=\"http://www.w3.org/2000/svg\" version=\"1.1\" ");
            sb.Append("width=\"2000\" height=\"2000\" ");
            sb.Append("viewBox=\"");
            sb.Append("129024 -18432 36864 36864");
            /*
            sb.Append(RootRegion.Min.X + " ");
            sb.Append(RootRegion.Min.Y + " ");
            sb.Append((RootRegion.Max.X - RootRegion.Min.X) + " ");
            sb.Append((RootRegion.Max.Y - RootRegion.Min.Y));
            */
            sb.AppendLine("\">");
            WriteNode(sb, RootRegion, scale);
            sb.AppendLine("</svg>");
            File.WriteAllText("regions.svg", sb.ToString());
        }

        private void WriteNode(StringBuilder sb, L2Region region, int scale)
        {
            sb.Append("<rect x=\"");
            sb.Append(region.Min.X / scale);
            sb.Append("\" y=\"");
            sb.Append(region.Min.Y / scale);
            sb.Append("\" width=\"");
            sb.Append((region.Max.X - region.Min.X) / scale);
            sb.Append("\" height=\"");
            sb.Append((region.Max.Y - region.Min.Y) / scale);
            sb.AppendLine("\" fill=\"none\" stroke-width=\"2\" stroke=\"black\" />");

            if (region.Children == null)
            {
                foreach(L2Object obj in region.Objects.Values)
                {
                    if (obj is Character && (obj as Character).Controller is GameClient)
                    {
                        sb.Append("<circle cx=\"");
                        sb.Append(obj.Position.X / scale);
                        sb.Append("\" cy=\"");
                        sb.Append(obj.Position.Y / scale);
                        sb.AppendLine("\" r=\"40\" stroke-width=\"0\" fill=\"purple\" />");

                        sb.Append("<text x=\"");
                        sb.Append((obj.Position.X + 10)/ scale);
                        sb.Append("\" y=\"");
                        sb.Append(obj.Position.Y / scale);
                        sb.AppendLine("\" fill=\"purple\">" + (obj as Character).Name + "</text>");
                    }
                    else
                    {
                        sb.Append("<circle cx=\"");
                        sb.Append(obj.Position.X / scale);
                        sb.Append("\" cy=\"");
                        sb.Append(obj.Position.Y / scale);
                        sb.AppendLine("\" r=\"20\" stroke-width=\"0\" fill=\"red\" />");
                    }
                }
            }
            else
            {
                foreach (var child in region.Children)
                    WriteNode(sb, child, scale);
            }
        }

        public void AddObject(L2Object obj)
        {
            RootRegion.Insert(obj.Position, obj);

            if (obj is INotifyObject)
            {
                (obj as INotifyObject).Broadcasting -= L2World_Broadcasting;
                (obj as INotifyObject).Broadcasting += L2World_Broadcasting;

                if (obj is Character && (obj as Character).Controller is GameClient)
                    InformObject(obj as Character);
            }
        }

        public void TransferObject(Character character, Vector3 pos, Vector3 oldPos)
        {
            RootRegion.Insert(pos, character);
            InformMoved(character, pos, oldPos);
        }

        public void RemoveObject(L2Object obj)
        {
            L2Object obj2;

            if (obj is INotifyObject)
            {
                (obj as INotifyObject).Broadcasting -= L2World_Broadcasting;
            }
        }

        private void InformObject(Character character)
        {
            var packet = new S_0x31_CharInfo(character).WriteSilent();
            RootRegion.InformAdded(character.Position, character, packet);
        }

        public void InformMoved(Character character, Vector3 pos, Vector3 oldPos)
        {
            var msgAdded = new S_0x31_CharInfo(character).WriteSilent();
            var msgDeleted = new S_0x08_DeleteObject(character.ObjectId).WriteSilent();
            RootRegion.InformMoved(pos, oldPos, character, msgAdded, msgDeleted);
        }

        private void L2World_Broadcasting(object sender, ISendableMessage e)
        {
            Character character = sender as Character;
            
            if (character != null)
                RootRegion.Broadcast(character.Position, e);


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
