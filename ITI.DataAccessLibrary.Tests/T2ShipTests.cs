using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ITI.DataAccessLibrary.Correction.Model;
using ITI.DataAccessLibrary.Correction;

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
        public void t1_get_All_Ships()
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
        [Test]
        public void t2_get_correct_ship_by_id()
        {
            //Arrange
            generator.CreateDatabase();
            ShipQueries sut = new ShipQueries();

            //Act
            ContainerShip genData = generator.ContainerShips.FirstOrDefault();
            ContainerShip data = sut.GetShipById(genData.Id);

            //Assert
            Assert.AreEqual(genData.Id, data.Id);
            Assert.AreEqual(genData.Name, data.Name);
            Assert.AreEqual(genData.Crew, data.Crew);
        }

        [Test]
        public void t3_get_ship_by_crew_size_return_list_in_right_order()
        {
            //Arrange
            generator.CreateDatabase();
            ShipQueries sut = new ShipQueries();

            //Act
            List<ContainerShip> data = sut.GetShipsByCrew();
            List<ContainerShip> sortgen = generator.ContainerShips.OrderBy(s => s.Crew).ToList();
            data.OrderBy(d => d.Crew);

            //Assert
            for (int i = 0; i < generator.ContainerShips.Count; i++)
            {
                Assert.AreEqual(sortgen[i].Crew, data[i].Crew);
            }
        }
    }
}

