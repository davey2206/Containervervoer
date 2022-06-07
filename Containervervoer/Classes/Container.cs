using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containervervoer.Classes
{
    public enum enumContent
    {
        Coolable = 0,
        Normal = 1,
        Valuble = 2,
    }

    public class ShipContainer
    {
        private int id;
        private int weight;
        private enumContent content;
        private int x;
        private int y;
        private int z;

        public int ID
        { get { return id; } }

        public int Weight
        { get { return weight; } }

        public enumContent Content
        { get { return content; } }

        public int X
        { get { return x; } }

        public int Y
        { get { return y; } }

        public int Z
        { get { return z; } }

        public ShipContainer(int i, int W, enumContent C)
        {
            id = i;
            weight = W + 4; //weight in tons
            content = C;
        }

        public bool CheckWeight()
        {
            if (weight > 30)
            {
                return true;
            }
            return false;
        }

        public void SetCord(int corX, int corY, int corZ)
        {
            x = corX;
            y = corY;
            z = corZ;
        }
    }
}