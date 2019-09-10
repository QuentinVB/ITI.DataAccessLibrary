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
            Assert.True(!generator.Harbors.Except(data).Any());
        }       
    }
}

