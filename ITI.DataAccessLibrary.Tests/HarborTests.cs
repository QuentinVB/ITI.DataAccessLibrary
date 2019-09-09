using NUnit.Framework;

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
            Assert.Pass();
        }
    }
}

