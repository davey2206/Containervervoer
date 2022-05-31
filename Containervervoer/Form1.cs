using Containervervoer.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Containervervoer
{
    public partial class Form1 : Form
    {
        List<ShipContainer> containers = new List<ShipContainer>();
        public Form1()
        {
            InitializeComponent();
            containers.Add(new ShipContainer(1, enumContent.Normal));
            containers.Add(new ShipContainer(1, enumContent.Normal));
            containers.Add(new ShipContainer(1, enumContent.Normal));
            containers.Add(new ShipContainer(1, enumContent.Normal));
            containers.Add(new ShipContainer(1, enumContent.Normal));
            containers.Add(new ShipContainer(1, enumContent.Normal));
            containers.Add(new ShipContainer(1, enumContent.Normal));
            containers.Add(new ShipContainer(1, enumContent.Normal));
            containers.Add(new ShipContainer(1, enumContent.Normal));
            containers.Add(new ShipContainer(1, enumContent.Normal));
            containers.Add(new ShipContainer(1, enumContent.Normal));
            containers.Add(new ShipContainer(1, enumContent.Normal));
            containers.Add(new ShipContainer(1, enumContent.Normal));
            containers.Add(new ShipContainer(1, enumContent.Normal));
            containers.Add(new ShipContainer(1, enumContent.Normal));
            containers.Add(new ShipContainer(1, enumContent.Normal));
            containers.Add(new ShipContainer(1, enumContent.Normal));
            containers.Add(new ShipContainer(1, enumContent.Normal));
            containers.Add(new ShipContainer(1, enumContent.Normal));
            containers.Add(new ShipContainer(1, enumContent.Normal));
            containers.Add(new ShipContainer(1, enumContent.Normal));
            containers.Add(new ShipContainer(1, enumContent.Normal));
            containers.Add(new ShipContainer(1, enumContent.Normal));
            containers.Add(new ShipContainer(1, enumContent.Normal));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ship ship = new Ship(100, 5, 3);
            ship.AddContainer(containers);
            ship.CalCord();
            var containers_ship = ship.Containers;

            foreach (var container in containers_ship)
            {
                int x = (container.X * 150) - (container.Z * 5);
                int y = (container.Y * 150) - (container.Z * 5);
                ListBox listBox = new ListBox();
                listBox.Location = new System.Drawing.Point(x, y);
                listBox.Size = new System.Drawing.Size(50, 100);

                ShipPanel.Controls.Add(listBox);
            }
        }
    }
}
