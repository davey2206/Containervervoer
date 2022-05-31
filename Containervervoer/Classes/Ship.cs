using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containervervoer.Classes
{
    public class Ship
    {
        private int maxWeight;
        private int weight_L;
        private int weight_R;
        private int length;
        private int width;
        private List<ShipContainer> containers = new List<ShipContainer>();

        public int MaxWeight { get { return maxWeight; } }
        public int Weight_L { get { return weight_L; } }
        public int Weight_R { get { return weight_R; } }
        public int Length { get { return length; } }
        public int Width { get { return width; } }
        public List<ShipContainer> Containers { get { return containers; } }

        public Ship(int mWeight, int L, int W)
        {
            maxWeight = mWeight;
            length = L;
            width = W;
        }

        public void AddContainer(List<ShipContainer> containersToAdd)
        {
            foreach (var container in containersToAdd)
            {
                if (CheckWeight(container))
                {
                    containers.Add(container);
                }
            }
        }

        public bool CheckWeight(ShipContainer containerToAdd)
        {
            int W = 0;
            if (containers != null)
            {
                foreach (var container in containers)
                {
                    W = W + container.Weight;
                }
            }

            W = W + containerToAdd.Weight;

            if (W > maxWeight)
            {
                return false;
            }

            return true;
        }

        public void CalCord()
        {
            int x = 1;
            int y = 1;
            int z = 0;

            foreach (var container in containers)
            {
                if (checkStack(x, y, container) == false)
                {
                    while (checkStack(x, y, container) == false)
                    {
                        if (x != width)
                        {
                            x++;
                        }
                        else
                        {
                            x = 1;
                            y++;
                        }

                        if (y == length + 1)
                        {
                            x = 1;
                            y = 1;
                            z++;
                        }
                    }
                }

                container.SetCord(x, y, z);

                if (x != width)
                {
                    x++;
                }
                else
                {
                    x = 1;
                    y++;
                }

                if (y == length + 1)
                {
                    x = 1;
                    y = 1;
                    z++;
                }
            }
        }

        public bool checkStack(int x, int y, ShipContainer C)
        {
            int W = 0;
            List<ShipContainer> shipContainers = containers.Where(c => c.X == x && c.Y == y).ToList();
            foreach (var shipContainer in shipContainers)
            {
                if (shipContainer.Z != 0)
                {
                    W = W + shipContainer.Weight;
                }
            }

            W = W + C.Weight;

            if (W > 120)
            {
                return false;
            }

            return true;
        }
    }
}
