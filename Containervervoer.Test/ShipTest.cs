using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Containervervoer.Classes;
using System.Collections.Generic;
using System.Linq;

namespace Containervervoer.Test
{
    [TestClass]
    public class ShipTest
    {
        [TestMethod]
        public void MaxWeightContainerTest()
        {
            //arrange
            Ship ship = new Ship(40, 2, 2);
            List<ShipContainer> containers = new List<ShipContainer>();

            containers.Add(new ShipContainer(1, 26, enumContent.Normal));

            //Act
            ship.AddContainer(containers);

            //Assert
            Assert.IsNull(ship.TextLog);
        }

        [TestMethod]
        public void MaxWeightContainerBADTest()
        {
            //arrange
            Ship ship = new Ship(40, 2, 2);
            List<ShipContainer> containers = new List<ShipContainer>();

            containers.Add(new ShipContainer(1, 30, enumContent.Normal));

            //Act
            ship.AddContainer(containers);

            //Assert
            Assert.IsNotNull(ship.TextLog);
        }

        [TestMethod]
        public void MaxWeightShipTest()
        {
            //arrange
            Ship ship = new Ship(120, 2, 2);
            List<ShipContainer> containers = new List<ShipContainer>();

            for (int i = 0; i < 5; i++)
            {
                if (i < 4)
                {
                    containers.Add(new ShipContainer(i, 26, enumContent.Normal));
                }
            }

            //Act
            ship.AddContainer(containers);

            //Assert
            Assert.IsNull(ship.TextLog);
        }

        [TestMethod]
        public void MaxWeightShipBADTest()
        {
            //arrange
            Ship ship = new Ship(120, 2, 2);
            List<ShipContainer> containers = new List<ShipContainer>();

            for (int i = 0; i < 5; i++)
            {
                containers.Add(new ShipContainer(i, 26, enumContent.Normal));
            }

            //Act
            ship.AddContainer(containers);

            //Assert
            Assert.IsNotNull(ship.TextLog);
        }

        [TestMethod]
        public void MinWeightShipTest()
        {
            //arrange
            Ship ship = new Ship(120, 2, 2);
            List<ShipContainer> containers = new List<ShipContainer>();

            for (int i = 0; i < 4; i++)
            {
                containers.Add(new ShipContainer(i, 26, enumContent.Normal));
            }

            //Act
            ship.AddContainer(containers);

            //Assert
            Assert.IsNull(ship.TextLog);
        }

        [TestMethod]
        public void MinWeightShipBADTest()
        {
            //arrange
            Ship ship = new Ship(120, 2, 2);
            List<ShipContainer> containers = new List<ShipContainer>();

            for (int i = 0; i < 4; i++)
            {
                containers.Add(new ShipContainer(i, 1, enumContent.Normal));
            }

            //Act
            ship.AddContainer(containers);

            //Assert
            Assert.IsNotNull(ship.TextLog);
        }

        [TestMethod]
        public void ValubleStackTest()
        {
            //arange
            Ship ship = new Ship(400, 2, 2);
            List<ShipContainer> containers = new List<ShipContainer>();

            containers.Add(new ShipContainer(1, 26, enumContent.Normal));
            containers.Add(new ShipContainer(2, 26, enumContent.Normal));
            containers.Add(new ShipContainer(3, 26, enumContent.Normal));
            containers.Add(new ShipContainer(4, 26, enumContent.Normal));
            containers.Add(new ShipContainer(5, 26, enumContent.Valuble));
            containers.Add(new ShipContainer(6, 26, enumContent.Valuble));
            containers.Add(new ShipContainer(7, 26, enumContent.Valuble));
            containers.Add(new ShipContainer(8, 26, enumContent.Valuble));

            //Act
            ship.AddContainer(containers);

            //Assert
            foreach (var container in ship.Containers)
            {
                if (container.Content == enumContent.Valuble)
                {
                    Assert.AreEqual(container.Z, 1);
                }
                else
                {
                    Assert.AreEqual(container.Z, 0);
                }
            }
        }

        [TestMethod]
        public void ValublePlacementTest()
        {
            //arange
            Ship ship = new Ship(400, 3, 2);
            List<ShipContainer> containers = new List<ShipContainer>();

            containers.Add(new ShipContainer(1, 26, enumContent.Normal));
            containers.Add(new ShipContainer(2, 26, enumContent.Normal));
            containers.Add(new ShipContainer(3, 26, enumContent.Normal));
            containers.Add(new ShipContainer(4, 26, enumContent.Normal));
            containers.Add(new ShipContainer(5, 26, enumContent.Valuble));
            containers.Add(new ShipContainer(6, 26, enumContent.Valuble));
            containers.Add(new ShipContainer(7, 26, enumContent.Valuble));
            containers.Add(new ShipContainer(8, 26, enumContent.Valuble));

            //Act
            ship.AddContainer(containers);

            //Assert
            foreach (var container in ship.Containers)
            {
                if (container.Content == enumContent.Valuble)
                {
                    Assert.AreNotEqual(container.Y, 2);
                }

                Assert.IsFalse(ship.Containers.Where(c => c.X == container.X && c.Y == container.Y && c.Content == enumContent.Valuble).Count() > 1);
            }
        }

        [TestMethod]
        public void CoolablePlacementTest()
        {
            //arrange
            Ship ship = new Ship(120, 2, 2);
            List<ShipContainer> containers = new List<ShipContainer>();

            containers.Add(new ShipContainer(1, 26, enumContent.Coolable));
            containers.Add(new ShipContainer(2, 26, enumContent.Coolable));
            containers.Add(new ShipContainer(3, 26, enumContent.Coolable));
            containers.Add(new ShipContainer(3, 26, enumContent.Coolable));

            //Act
            ship.AddContainer(containers);

            //Assert
            foreach (var container in ship.Containers)
            {
                Assert.AreEqual(container.Y, 1);
            }
        }

        [TestMethod]
        public void BalanceTest()
        {
            //arrange
            Ship ship = new Ship(120, 2, 2);
            List<ShipContainer> containers = new List<ShipContainer>();

            containers.Add(new ShipContainer(1, 26, enumContent.Normal));
            containers.Add(new ShipContainer(2, 1, enumContent.Normal));
            containers.Add(new ShipContainer(3, 26, enumContent.Normal));

            //Act
            ship.AddContainer(containers);

            string[] dif = ship.TextDifference.Split(' ');
            double test = double.Parse(dif[0]);
            //Assert
            Assert.IsTrue(test < 20);
        }
    }
}