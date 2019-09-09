using ITI.DataAccessLibrary.Correction;
using ITI.DataAccessLibrary.Model;
using NUnit.Framework;
using System.Collections.Generic;

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
        public void Test1()
        {
            generator.CreateDatabase();
            HarborQueries harborQueries = new HarborQueries();

            List<Harbor> harbors = harborQueries.GetAllHarbor();

            Assert.Pass();
        }
    }
}

