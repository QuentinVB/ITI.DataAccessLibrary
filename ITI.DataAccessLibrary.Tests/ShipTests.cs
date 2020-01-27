using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ITI.DataAccessLibrary;
using ITI.DataAccessLibrary.Model;

namespace ITI.DataAccessLibrary.Tests
{
    public class T2ShipTests
    {
        DBGenerator generator;
        [SetUp]
        public void Setup()
        {
            generator = new DBGenerator();
            generator.CreateDatabase();
        }

        //TODO : Create, Update, Delete Ship

        [Test]
        public void t1_getAllShips()
        {
            //Arrange
            ShipQueries sut = new ShipQueries();

            //Act
            List<ContainerShip> data = sut.GetAllShips();

            //Assert
            Assert.AreEqual(generator.ContainerShips.Count, data.Count);

            generator.ContainerShips.OrderBy(s => s.Id);
            data.OrderBy(s => s.Id);

            for (int i = 0; i < generator.ContainerShips.Count; i++)
            {
                Assert.AreEqual(generator.ContainerShips[i].Name, data[i].Name);
            }
        } 
    }
}

