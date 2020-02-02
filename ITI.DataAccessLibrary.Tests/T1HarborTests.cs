using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ITI.DataAccessLibrary;
using ITI.DataAccessLibrary.Model;

namespace ITI.DataAccessLibrary.Tests
{
    public class T1HarborTests
    {
        DBGenerator generator;
        [SetUp]
        public void Setup()
        {
            generator = new DBGenerator();
            generator.CreateDatabase();
        }
        //TODO : Update Harbor

        /// <summary>
        /// test if the GetAllHarbor method return all the harbors
        /// </summary>
        [Test]
        public void t1_the_harbors_can_be_all_get()
        {
            //Arrange
            
            HarborQueries sut = new HarborQueries();

            //Act
            List<Harbor> data = sut.GetAllHarbor();

            //Assert
            Assert.AreEqual(generator.Harbors.Count, data.Count);
            generator.Harbors.OrderBy(h => h.Id);
            data.OrderBy(h => h.Id);

            for (int i = 0; i < generator.Harbors.Count; i++)
            {
                Assert.AreEqual(generator.Harbors[i].Name, data[i].Name);
                Assert.AreEqual(generator.Harbors[i].Country, data[i].Country);
                Assert.That(generator.Harbors[i].Latitude, Is.EqualTo(data[i].Latitude).Within(0.00001));
                Assert.That(generator.Harbors[i].Longitude, Is.EqualTo(data[i].Longitude).Within(0.00001));
            }
        }
        /// <summary>
        /// test if the GetHarborByCountry method return the harbors of a specific country
        /// </summary>
        [Test]
        public void t2_get_Harbor_By_Country()
        {
            //Arrange
            HarborQueries sut = new HarborQueries();

            //Act
            List<Harbor> genData = generator.Harbors.OrderBy(h => h.Country ).ToList();
            List<Harbor> data = sut.GetHarborByCountry();

            //Assert
            Assert.AreEqual(genData.Count, data.Count);
            for(int i = 0; i < genData.Count; i++)
            {
                Assert.AreEqual(genData[i].Country, data[i].Country);
                Assert.AreEqual(genData[i].Name, data[i].Name);
            }       
        }

        /// <summary>
        /// test if the GetHarborById method return the specific harbor
        /// </summary>
        [Test]
        public void t3_can_get_correct_harbor_by_id()
        {
            //Arrange
            HarborQueries sut = new HarborQueries();

            //Act
            Harbor genData = generator.Harbors.FirstOrDefault();
            Harbor data = sut.GetHarborById(genData.Id);

            //Assert
            Assert.AreEqual(genData.Name, data.Name);
            Assert.AreEqual(genData.Country, data.Country);
            Assert.That(genData.Latitude, Is.EqualTo(data.Latitude).Within(0.00001));
            Assert.That(genData.Longitude, Is.EqualTo(data.Longitude).Within(0.00001));
        }

        //TODO : test insert
    }
}

