using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ITI.DataAccessLibrary.Correction.Model;
using ITI.DataAccessLibrary.Correction;

namespace ITI.DataAccessLibrary.Tests
{
    public class T3ContainerTests
    {
        DBGenerator generator;
        [SetUp]
        public void Setup()
        {
            generator = new DBGenerator();
            generator.CreateDatabase();
        }

        /// <summary>
        /// test if the method GetAllContainers return all the containers
        /// </summary>
        [Test]
        public void t1_get_All_Containers()
        {
            //Arrange
            ContainerQueries sut = new ContainerQueries();

            //Act
            List<Container> data = sut.GetAllContainers();

            //Assert
            Assert.AreEqual(generator.Containers.Count, data.Count);

            generator.Containers.OrderBy(s => s.Id);
            data.OrderBy(s => s.Id);

            for (int i = 0; i < generator.Containers.Count; i++)
            {
                Assert.AreEqual(generator.Containers[i].Reference, data[i].Reference);
            }
        }
        /// <summary>
        /// test if the method GetContainersFromShip return the container list of a specific ship
        /// </summary>
        [Test]
        public void t2_get_container_from_a_ship()
        {
            //Arrange
            ContainerQueries sut = new ContainerQueries();
            int shipId = generator.RandomSource.Next(1, generator.ContainerShips.Count);
            int generatedContainerCount =
                generator.Containers.Count(
                c => c.CurrentShip.Id == shipId
                );

            //Act
            List<Container> data = sut.GetContainersFromShip(shipId);

            //Assert
            Assert.AreEqual(generatedContainerCount, data.Count);

            for (int i = 0; i < data.Count; i++)
            {
                Assert.AreEqual(data[i].CurrentShip.Id , shipId);
            }
        }

        /// <summary>
        /// test if the GetMerchandisesSummary method return a list of MerchandisesSummary (Content, container count and total weight) ordered to the transported mass to the highest
        /// </summary>
        [Test]
        public void t3_get_summary_of_merchandise_around_the_world_by_weight_asc()
        {
            //Arrange
            ContainerQueries sut = new ContainerQueries();
            List<MerchandiseSummary> genContainersSummary = (
                from c in generator.Containers
                group c by c.Content into grouping
                select new MerchandiseSummary
                {
                    Content = grouping.Key,
                    TotalContainerCount = grouping.Count(),
                    TotalWeight = grouping.Sum(s => s.Weight),
                } into s
                orderby s.TotalWeight ascending
                select s
                ).ToList();

            //Act
            List<MerchandiseSummary> data = sut.GetMerchandisesSummary();

            //Assert
            Assert.AreEqual(genContainersSummary.Count, data.Count);

            for (int i = 0; i < data.Count; i++)
            {
                Assert.AreEqual(genContainersSummary[i].Content, data[i].Content);
                Assert.AreEqual(genContainersSummary[i].TotalContainerCount, data[i].TotalContainerCount);
                Assert.AreEqual(genContainersSummary[i].TotalWeight, data[i].TotalWeight);
            }
        }
    }
}

