using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containervervoer.Classes
{
    public enum enumContent
    {
        Normal,
        Valuble,
        Cooable
    }

    public class ShipContainer
    {
        private int weight;
        private enumContent content;
        private int x;
        private int y;
        private int z;

        public int Weight { get { return weight; } }
        public enumContent Content { get { return content; } }
        public int X { get { return x; } }
        public int Y { get { return y; } }
        public int Z { get { return z; } }

        public ShipContainer(int W, enumContent C)
        {
            weight = W + 4; //weight in tons
            content = C;
        }
        
        public void SetCord(int corX, int corY, int corZ)
        {
            x = corX;
            y = corY;
            z = corZ;
        }
    }
}
