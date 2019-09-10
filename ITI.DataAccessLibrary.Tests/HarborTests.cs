using ITI.DataAccessLibrary.Correction;
using ITI.DataAccessLibrary.Correction.Model;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ITI.DataAccessLibrary.Tests
{
    public class Tests
    {
        DBGenerator generator;
        [SetUp]
        public void Setup()
        {
            generator = new DBGenerator();
        }

        [Test]
        public void t1_getAllHarbors()
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
    }
}

