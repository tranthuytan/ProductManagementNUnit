using NUnit.Framework;
using ProductManagement;

namespace NUnitTest1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            ProductRepository productRepo = new ProductRepository();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}