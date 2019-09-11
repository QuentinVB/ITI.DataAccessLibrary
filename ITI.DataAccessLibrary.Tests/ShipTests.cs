using ITI.DataAccessLibrary.Correction;
using ITI.DataAccessLibrary.Correction.Model;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ITI.DataAccessLibrary.Tests
{
    public class T2ShipTests
    {
        DBGenerator generator;
        [SetUp]
        public void Setup()
        {
            generator = new DBGenerator();
        }

        [Test]
        public void t1_getAllShips()
        {
            //Arrange
            generator.CreateDatabase();
            ShipQueries sut = new ShipQueries();

            //Act
            List<ContainerShip> data = sut.GetAllShips();

            //Assert
            Assert.AreEqual(generator.ContainerShips.Count, data.Count);

            generator.ContainerShips.Sort((x, y) =>
            {
                return y.Id.CompareTo(x.Id);
            });
            data.Sort((x, y) =>
            {
                return y.Id.CompareTo(x.Id);
            });

            for (int i = 0; i < generator.ContainerShips.Count; i++)
            {
                Assert.AreEqual(generator.Harbors[i].Name, data[i].Name);
            }

        }       
    }
}

