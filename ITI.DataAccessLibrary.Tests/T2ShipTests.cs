using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ITI.DataAccessLibrary;
using ITI.DataAccessLibrary.Model;
using System;

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
            ShipQueries sut = new ShipQueries();

            //Act
            ContainerShip genData = generator.ContainerShips.FirstOrDefault();
            ContainerShip data = sut.GetShipById(genData.Id);

            //Assert
            Assert.AreEqual(genData.Id, data.Id);
            Assert.AreEqual(genData.ATISCode, data.ATISCode);
            Assert.AreEqual(genData.Name, data.Name);
            Assert.AreEqual(genData.DepartureTime, data.DepartureTime);
            Assert.AreEqual(genData.ArrivalTime, data.ArrivalTime);
            Assert.AreEqual(genData.Crew, data.Crew);
            Assert.AreEqual(genData.MaxHeight, data.MaxHeight);
            Assert.AreEqual(genData.MaxLength, data.MaxLength);

            Assert.That(genData.MaxSpeed, Is.EqualTo(data.MaxSpeed).Within(0.001));

            Assert.AreEqual(genData.MaxWeight, data.MaxWeight);
            Assert.AreEqual(genData.MaxWidth, data.MaxWidth);
        }

        [Test]
        public void t3_get_ship_by_crew_size_return_list_in_right_order()
        {
            //Arrange
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

        [Test]
        public void t4_insert_ship()
        {
            //Arrange
            ShipQueries sut = new ShipQueries();
            ContainerShip testShip = new ContainerShip()
            {
                Id = 61,
                ATISCode = "gyugyctrcfcft456",
                Name = "dsfdsfv",
                Origin = new Harbor() { Id = 100 },
                Destination = new Harbor() { Id = 101 },
                DepartureTime = new System.DateTime(1992, 08, 12),
                ArrivalTime = new System.DateTime(1992, 08, 12),
                Cargo = new List<Container>(),
                Crew = 6,
                MaxHeight = 30,
                MaxLength = 220,
                MaxSpeed = 20,
                MaxWeight = 33000,
                MaxWidth = 20
            };

            //Act
            sut.InsertShip(testShip);
            var control = sut.GetShipById(testShip.Id);

            //Assert
            Assert.NotNull(control);
            Assert.AreEqual(testShip.Id, control.Id);
            Assert.AreEqual(testShip.ATISCode, control.ATISCode);
            Assert.AreEqual(testShip.Name, control.Name);
            Assert.AreEqual(testShip.DepartureTime, control.DepartureTime);
            Assert.AreEqual(testShip.ArrivalTime, control.ArrivalTime);
            Assert.AreEqual(testShip.Crew, control.Crew);
            Assert.AreEqual(testShip.MaxHeight, control.MaxHeight);
            Assert.AreEqual(testShip.MaxLength, control.MaxLength);
            Assert.That(testShip.MaxSpeed, Is.EqualTo(control.MaxSpeed).Within(0.001));
            Assert.AreEqual(testShip.MaxWeight, control.MaxWeight);
            Assert.AreEqual(testShip.MaxWidth, control.MaxWidth);
        }



        [Test]
        public void t5_delete_ship()
        {

            //Arrange
            ContainerShip testShip = new ContainerShip()
            {
                Id = 50,
                ATISCode = Guid.NewGuid().ToString(),
                Name = generator.GetRandomName(),
                Origin = new Harbor() { Id = 100 },
                Destination = new Harbor() { Id = 101 },
                DepartureTime = new System.DateTime(1992, 08, 12),
                ArrivalTime = new System.DateTime(1992, 08, 12),
                Cargo = new List<Container>(),
                Crew = 6,
                MaxHeight = 30,
                MaxLength = 220,
                MaxSpeed = 20,
                MaxWeight = 33000,
                MaxWidth = 20
            };
            ShipQueries sut = new ShipQueries();          

            //Act
            sut.InsertShip(testShip);

            sut.DeleteShip(testShip);

            var control = sut.GetShipById(testShip.Id);

            //Assert
            Assert.Null(control);
           
        }
    }
}

