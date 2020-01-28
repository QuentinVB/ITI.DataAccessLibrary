using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ITI.DataAccessLibrary.Correction;
using ITI.DataAccessLibrary.Correction.Model;
using System;

namespace ITI.DataAccessLibrary.Tests
{
    public class T4ShipRedux
    {
        DBGenerator generator;
        [SetUp]
        public void Setup()
        {
            generator = new DBGenerator();
            generator.CreateDatabase();
        }

        //TODO : Create, Update, Delete Ship redux

        [Test]
        public void t1_getAllShipReduxWithContainerCount()
        {
            //Arrange
            ShipQueries sut = new ShipQueries();

            //Act
            List<ContainerShipRedux> data = sut.GetAllShipsRedux();

            //Assert
            var genGroupedContainers =
                from c in generator.Containers
                group c by c.CurrentShip.Id into grouping
                select new
                {
                    ShipId = grouping.Key,
                    ContainerCount = grouping.Count(),
                };

           
            foreach (var item in genGroupedContainers)
            {
                Assert.AreEqual(
                    data.Where(sr => sr.Id == item.ShipId).First().ContainerCount,
                    item.ContainerCount);
            }
        }
        [Test]
        public void t2_getAllShipReduxWithWeightLoad()
        {
            //Arrange
            ShipQueries sut = new ShipQueries();

            //Act
            List<ContainerShipRedux> data = sut.GetAllShipsRedux();

            //Assert
            var genGroupedContainers =
                from c in generator.Containers
                group c by c.CurrentShip.Id into grouping
                select new
                {
                    ShipId = grouping.Key,
                    //ContainerCount = c.Count(),
                    WeightSum = grouping.Sum(s => s.LoadWeigth),
                };
            
            foreach (var item in genGroupedContainers)
            {
                Assert.AreEqual(
                    data.Where(sr => sr.Id == item.ShipId).First().TotalWeightLoad,
                    item.WeightSum);
            }
        }        
    }
}

