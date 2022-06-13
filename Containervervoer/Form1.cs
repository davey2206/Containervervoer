using Containervervoer.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Containervervoer
{
    public partial class Form1 : Form
    {
        private List<ShipContainer> containers = new List<ShipContainer>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShipPanel.Controls.Clear();
            containers.Clear();
            containers.Add(new ShipContainer(41, 26, enumContent.Valuble));
            containers.Add(new ShipContainer(41, 1, enumContent.Valuble));
            containers.Add(new ShipContainer(41, 26, enumContent.Valuble));

            Random rng = new Random();
            for (int i = 0; i < 40; i++)
            {
                switch (rng.Next(5))
                {
                    case 0:
                        containers.Add(new ShipContainer(i, 1, enumContent.Normal));
                        break;

                    case 1:
                        containers.Add(new ShipContainer(i, 1, enumContent.Normal));
                        break;

                    case 2:
                        containers.Add(new ShipContainer(i, 1, enumContent.Normal));
                        break;

                    case 3:
                        containers.Add(new ShipContainer(i, 1, enumContent.Normal));
                        break;

                    case 4:
                        containers.Add(new ShipContainer(i, 1, enumContent.Coolable));
                        break;

                    case 5:
                        containers.Add(new ShipContainer(i, 26, enumContent.Valuble));
                        break;
                }
            }

            Ship ship = new Ship(400, 6, 2);
            ship.AddContainer(containers);

            int j = 0;
            foreach (var container in ship.Containers)
            {
                int x = (container.X * 150) + (container.Z * 5);
                int y = (container.Y * 150) + (container.Z * 5);
                if (container.Z == 100)
                {
                    x = 0;
                    y = 0;
                }
                if (x == 0 && y == 0)
                {
                    j = j + 5;
                    x = j;
                    y = j;
                }
                ListBox listBox = new ListBox();
                if (container.Content == enumContent.Coolable)
                {
                    listBox.BackColor = Color.Aqua;
                }
                if (container.Content == enumContent.Valuble)
                {
                    listBox.BackColor = Color.Gold;
                }
                listBox.Location = new System.Drawing.Point(x, y);
                listBox.Size = new System.Drawing.Size(50, 100);

                ShipPanel.Controls.Add(listBox);
                listBox.BringToFront();

                Thread.Sleep(10);
            }

            if (ship.TextLog != null)
            {
                MessageBox.Show(ship.TextLog);
            }

            if (ship.TextDifference != null)
            {
                label1.Text = ship.TextDifference;
            }
        }
    }
}