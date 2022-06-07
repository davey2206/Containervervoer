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

        public int MaxWeight
        { get { return maxWeight; } }

        public int Weight_L
        { get { return weight_L; } }

        public int Weight_R
        { get { return weight_R; } }

        public int Length
        { get { return length; } }

        public int Width
        { get { return width; } }

        public List<ShipContainer> Containers
        { get { return containers; } }

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
                    if (container.CheckWeight())
                    {
                        Console.WriteLine("container to heavy");
                        break;
                    }

                    containers.Add(container);
                }
            }
            containers = containers.OrderBy(c => (int)(c.Content)).ToList();

            if (containers.Count() < containersToAdd.Count())
            {
                Console.WriteLine("containers too heavy");
            }
            else if (checkMinWeight())
            {
                Console.WriteLine("ship to light");
            }
            else
            {
                SetCord();
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

        public bool checkMinWeight()
        {
            int w = 0;

            foreach (var container in containers)
            {
                w = w + container.Weight;
            }

            if (w < (maxWeight / 2))
            {
                return true;
            }
            return false;
        }

        public void checkWeight_L_R()
        {
            double middel = width / 2;
            double totalWeight = 0;

            foreach (var container in containers)
            {
                totalWeight = totalWeight + container.Weight;
                if (container.X < middel)
                {
                    weight_L = weight_L + container.Weight;
                }
                if (container.X > middel)
                {
                    weight_R = weight_R + container.Weight;
                }
            }
        }

        public bool checkStack(int x, int y, ShipContainer containerToAdd)
        {
            int w = 0;
            foreach (var container in containers)
            {
                if (container.X == x && container.Y == y)
                {
                    w = w + container.Weight;
                }
            }
            w = w + containerToAdd.Weight;

            if (w > 120)
            {
                return true;
            }
            return false;
        }

        public bool checkStacks(ShipContainer containerToAdd)
        {
            int x = 1;
            int y = 1;
            int count = 0;
            bool end = true;
            while (end)
            {
                if (containerToAdd.Content == enumContent.Normal)
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
                    if (y == length && x == width)
                    {
                        end = false;
                    }
                }
                else if (containerToAdd.Content == enumContent.Coolable)
                {
                    if (x != width)
                    {
                        x++;
                    }
                    if (x == width)
                    {
                        end = false;
                    }
                }
                else if (containerToAdd.Content == enumContent.Valuble)
                {
                    if (x != width)
                    {
                        x++;
                    }
                    else
                    {
                        x = 1;
                        y = length;
                    }

                    if (y == length && x == width)
                    {
                        end = false;
                    }
                }

                int w = 0;
                foreach (var container in containers)
                {
                    if (container.X == x && container.Y == y)
                    {
                        w = w + container.Weight;
                    }
                }
                w = w + containerToAdd.Weight;

                if (w > 120)
                {
                    count++;
                }
            }

            if (containerToAdd.Content == enumContent.Normal)
            {
                if ((count + 1) == width * length)
                {
                    return true;
                }
            }
            else if (containerToAdd.Content == enumContent.Coolable)
            {
                if ((count + 1) == width)
                {
                    return true;
                }
            }
            else if (containerToAdd.Content == enumContent.Valuble)
            {
                if ((count + 1) == (width * 2))
                {
                    return true;
                }
            }

            return false;
        }

        public void SetCord()
        {
            int i = 0;

            while (containers.Where(c => c.X == 0 && c.Y == 0 && c.Z == 0).Count() != 0)
            {
                if (containers.Where(c => c.Content == enumContent.Valuble).Count() > (width * 2))
                {
                    Console.WriteLine("to many Valuble containers");
                    break;
                }

                if (checkStacks(containers[i]))
                {
                    Console.WriteLine("cant place anymore containers");
                    break;
                }
                PlaceCoolable(containers[i]);
                PlaceNormal(containers[i]);
                PlaceValuble(containers[i]);
                i++;
            }
        }

        public void PlaceNormal(ShipContainer container)
        {
            int x = 1;
            int y = 1;
            int z = 0;
            if (container.Content == enumContent.Normal)
            {
                while (containers.Where(c => c.X == x && c.Y == y && c.Z == z).Count() != 0)
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

                container.SetCord(x, y, z);
            }
        }

        public void PlaceCoolable(ShipContainer container)
        {
            int x = 1;
            int y = 1;
            int z = 0;
            if (container.Content == enumContent.Coolable)
            {
                while (containers.Where(c => c.X == x && c.Y == y && c.Z == z).Count() != 0)
                {
                    if (x != width)
                    {
                        x++;
                    }
                    else
                    {
                        x = 1;
                        z++;
                    }
                }

                container.SetCord(x, y, z);
            }
        }

        public void PlaceValuble(ShipContainer container)
        {
            int x = 1;
            int y = 1;
            int z = 0;
            int count = 0;
            bool back = false;
            if (container.Content == enumContent.Valuble)
            {
                while (containers.Where(c => c.X == x && c.Y == y && c.Z == z).Count() != 0)
                {
                    count = 0;
                    for (int i = 0; i < width; i++)
                    {
                        if (containers.Where(c => c.X == x + i && c.Y == y && c.Content == enumContent.Valuble).Count() == 1)
                        {
                            count++;
                        }

                        if (count == width)
                        {
                            back = true;
                        }
                        else
                        {
                            back = false;
                        }
                    }
                    if (back)
                    {
                        y = length;
                        z = 0;
                    }
                    if (checkStack(x, y, container))
                    {
                        x++;
                    }
                    else
                    {
                        if (x != width)
                        {
                            x++;
                        }
                        else
                        {
                            x = 1;
                            z++;
                        }
                    }
                }

                container.SetCord(x, y, z);
            }
        }
    }
}