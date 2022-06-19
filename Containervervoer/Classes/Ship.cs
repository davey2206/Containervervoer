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
        private string textLog;
        private string textDifference;

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

        public string TextLog
        { get { return textLog; } }

        public string TextDifference
        { get { return textDifference; } }

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
                        textLog = textLog + " container to heavy";
                        break;
                    }

                    containers.Add(container);
                }
            }
            containers = containers.OrderBy(c => (int)(c.Content)).ToList();

            if (containers.Count() < containersToAdd.Count())
            {
                textLog = textLog + " containers too heavy";
            }
            else if (checkMinWeight())
            {
                textLog = textLog + " ship to light";
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

        public bool checkWeight_L_R()
        {
            weight_L = 0;
            weight_R = 0;
            double totalWeight = 0;
            double middel = (double)width / 2;
            foreach (var container in containers)
            {
                totalWeight = totalWeight + container.Weight;
                if (width == 2)
                {
                    if (container.X == 1)
                    {
                        weight_L = weight_L + container.Weight;
                    }
                    if (container.X == 2)
                    {
                        weight_R = weight_R + container.Weight;
                    }
                }
                else
                {
                    if (container.X < Math.Ceiling(middel))
                    {
                        weight_L = weight_L + container.Weight;
                    }
                    if (container.X > Math.Ceiling(middel))
                    {
                        weight_R = weight_R + container.Weight;
                    }
                }
            }

            double difference = Math.Abs(weight_L - weight_R);
            double percentDifference = ((double)100 / totalWeight) * difference;
            percentDifference = Math.Round(percentDifference, 1);
            textDifference = percentDifference.ToString() + " % verschil";

            if (percentDifference > 20)
            {
                return false;
            }

            return true;
        }

        public void balance()
        {
            foreach (var container in containers)
            {
                if (checkWeight_L_R() == false)
                {
                    if (container.Content == enumContent.Normal)
                    {
                        balanceNormal(container);
                    }
                    if (container.Content == enumContent.Coolable)
                    {
                        balanceCoolable(container);
                    }
                    if (container.Content == enumContent.Valuble)
                    {
                        balanceValuble(container);
                    }
                }
            }
        }

        public void balanceCoolableHelper(bool direction, ShipContainer container)
        {
            double middel = (double)width / 2;
            int x = 1;
            int y = 1;
            int z = 0;
            bool stop = false;

            int R = (int)Math.Ceiling(middel) + 1;
            int L = (int)Math.Ceiling(middel) - 1;

            if (width == 2)
            {
                R = 2;
                L = 1;
            }
            if (direction)
            {
                x = L;
            }
            else
            {
                x = R;
            }

            while (checkStack(x, y, container))
            {
                if (direction)
                {
                    x--;
                }
                else
                {
                    x++;
                }

                if (x >= width)
                {
                    stop = true;
                    break;
                }
            }
            if (stop == false)
            {
                foreach (var shipContainer in containers.Where(c => c.X == x && c.Y == y && c.Z == z).ToList())
                {
                    z = shipContainer.Z + 1;
                    shipContainer.SetCord(x, y, z);
                }
            }

            z = 0;
            if (stop == false)
            {
                container.SetCord(x, y, z);
            }
        }

        public void balanceCoolable(ShipContainer container)
        {
            if (weight_L > weight_R)
            {
                balanceCoolableHelper(false, container);
            }
            else
            {
                balanceCoolableHelper(true, container);
            }
        }

        public void balanceValubleHelper(bool direction, ShipContainer container)
        {
            double middel = (double)width / 2;
            int x = 1;
            int y = 1;
            int z = 0;

            int R = (int)Math.Ceiling(middel) + 1;
            int L = (int)Math.Ceiling(middel) - 1;

            if (width == 2)
            {
                R = 2;
                L = 1;
            }

            if (direction)
            {
                x = L;
            }
            else
            {
                x = R;
            }

            while (checkStack(x, y, container) || containers.Where(c => c.X == x && c.Y == y && c.Content == enumContent.Valuble).Count() == 1)
            {
                if (direction)
                {
                    x--;
                }
                else
                {
                    x++;
                }
                if (x >= width)
                {
                    if (y == length)
                    {
                        textLog = textLog + " cant balance";
                        break;
                    }
                    y = length;
                    x = R;
                }
            }
            foreach (var shipContainer in containers.Where(c => c.X == x && c.Y == y && c.Z == z).ToList())
            {
                z = shipContainer.Z + 1;
                shipContainer.SetCord(x, y, z);
            }

            z = 0;
            container.SetCord(x, y, z);
        }

        public void balanceValuble(ShipContainer container)
        {
            if (weight_L > weight_R)
            {
                balanceValubleHelper(false, container);
            }
            else
            {
                balanceValubleHelper(true, container);
            }
        }

        public void balanceNormalHelper(bool direction, ShipContainer container)
        {
            double middel = (double)width / 2;
            int x = 1;
            int y = 1;
            int z = 0;

            int R = (int)Math.Ceiling(middel) + 1;
            int L = (int)Math.Ceiling(middel) - 1;

            if (width == 2)
            {
                R = 2;
                L = 1;
            }

            if (direction)
            {
                x = L;
            }
            else
            {
                x = R;
            }
            while (checkStack(x, y, container))
            {
                if (direction)
                {
                    x--;
                }
                else
                {
                    x++;
                }
                if (x >= width)
                {
                    if (y == length)
                    {
                        break;
                    }
                    y++;
                    x = R;
                }
            }
            foreach (var shipContainer in containers.Where(c => c.X == x && c.Y == y && c.Z == z).ToList())
            {
                z = shipContainer.Z + 1;
                shipContainer.SetCord(x, y, z);
            }

            z = 0;
            container.SetCord(x, y, z);
        }

        public void balanceNormal(ShipContainer container)
        {
            if (weight_L > weight_R)
            {
                balanceNormalHelper(false, container);
            }
            else
            {
                balanceNormalHelper(true, container);
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
                int w = 0;
                foreach (var container in containers)
                {
                    if (container.X == x && container.Y == y)
                    {
                        w = w + container.Weight;
                    }
                }
                w = w + containerToAdd.Weight;

                if (w > 120 || containers.Where(c => c.X == x && c.Y == y && c.Content == enumContent.Valuble).Count() == 1)
                {
                    count++;
                }

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
                if (count == (width * 2))
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
                    textLog = textLog + " to many Valuble containers";
                    break;
                }

                if (checkStacks(containers[i]))
                {
                    textLog = textLog + " cant place anymore containers";
                    break;
                }
                PlaceCoolable(containers[i]);
                PlaceNormal(containers[i]);
                PlaceValuble(containers[i]);
                i++;
            }

            if (width != 1 && textLog == null)
            {
                balance();
                toGround();
            }
            containers = containers.OrderBy(c => c.Z).ToList();
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

                    if (x > width)
                    {
                        x = 1;
                        y = length;
                        count = 0;
                        bool stop = false;
                        for (int i = 0; i < width; i++)
                        {
                            if (containers.Where(c => c.X == x + i && c.Y == y && c.Content == enumContent.Valuble).Count() == 1)
                            {
                                count++;
                            }

                            if (count == width)
                            {
                                stop = true;
                            }
                            else
                            {
                                stop = false;
                            }
                        }
                        if (stop)
                        {
                            z = 100;
                            textLog = textLog + " cant place anymore Valuble containers";
                            break;
                        }
                    }
                }

                container.SetCord(x, y, z);
            }
        }

        public void toGround()
        {
            int times = width * length;
            int x = 1;
            int y = 1;
            int z = 0;
            for (int i = 0; i < times; i++)
            {
                z = 0;
                foreach (var container in containers.Where(c => c.X == x && c.Y == y).ToList())
                {
                    container.SetCord(x, y, z);
                    z++;
                }
                if (x != width)
                {
                    x++;
                }
                else
                {
                    x = 1;
                    y++;
                }
            }
        }
    }
}