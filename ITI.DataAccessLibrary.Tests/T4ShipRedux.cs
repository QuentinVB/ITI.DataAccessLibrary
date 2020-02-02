using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ITI.DataAccessLibrary;
using ITI.DataAccessLibrary.Model;
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

        /// <summary>
        /// test if the GetAllShipsRedux method return the correct container count for each ship
        /// </summary>
        [Test]
        public void t1_get_All_Ship_Redux_With_correct_Container_Count()
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

        /// <summary>
        /// test if the GetAllShipsRedux method return the correct weight sum for each ship
        /// </summary>
        [Test]
        public void t2_get_All_Ship_Redux_With_Weight_Load()
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
                    WeightSum = grouping.Sum(s => s.Weight),
                };
            
            foreach (var item in genGroupedContainers)
            {
                Assert.AreEqual(
                    data.Where(sr => sr.Id == item.ShipId).First().TotalWeight,
                    item.WeightSum);
            }
        }        
    }
}

