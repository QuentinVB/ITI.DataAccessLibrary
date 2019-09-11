using ITI.DataAccessLibrary.Correction;
using ITI.DataAccessLibrary.Correction.Model;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ITI.DataAccessLibrary.Tests
{
    public class T1HarborTests
    {
        DBGenerator generator;
        [SetUp]
        public void Setup()
        {
            generator = new DBGenerator();
        }

        [Test]
        public void t1_the_harbors_can_be_all_get()
        {
            //Arrange
            generator.CreateDatabase();
            HarborQueries sut = new HarborQueries();

            //Act
            List<Harbor> data = sut.GetAllHarbor();

            //Assert
            Assert.AreEqual(generator.Harbors.Count, data.Count);

            generator.Harbors.Sort((x, y) =>
            {
                return y.Id.CompareTo(x.Id);
            });
            data.Sort((x, y) =>
            {
                return y.Id.CompareTo(x.Id);
            });

            for (int i = 0; i < generator.Harbors.Count; i++)
            {
                Assert.AreEqual(generator.Harbors[i].Name, data[i].Name);
                Assert.AreEqual(generator.Harbors[i].Country, data[i].Country);
                Assert.That(generator.Harbors[i].Latitude, Is.EqualTo(data[i].Latitude).Within(0.00001));
                Assert.That(generator.Harbors[i].Longitude, Is.EqualTo(data[i].Longitude).Within(0.00001));
            }
        }

        [Test]
        public void t2_getHarborByCountry()
        {
            //Arrange
            generator.CreateDatabase();
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

        [Test]
        public void t3_can_get_correct_harbor_by_id()
        {
            //Arrange
            generator.CreateDatabase();
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
    }
}

