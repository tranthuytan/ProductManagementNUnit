using NUnit.Framework;
using ProductManagement;
using System;

namespace NUnitTest
{
    [TestFixture]
    [Parallelizable]
    public class Tests
    {
        ProductRepository productRepo;
        Product product;
        [SetUp]
        public void Setup()
        {
            productRepo = new ProductRepository();
            product = new Product();
        }
        [TearDown]
        public void Teardown()
        {
            productRepo = null;
            product= null;
        }
        [Test,Order(1)]
        [TestCase(5, "Olong Tea Plus", "2022/06/18", 12000, 1, 1)]
        public void GetByIdTest_GetProduct(int id, string name, DateTime dt, double price, int sta, int ctid)
        {
            //System.Threading.Thread.Sleep(2000);
            //Assign
            product.Id = id;
            product.Name = name;
            product.CreateDate = dt;
            product.Price = price;
            product.Status = 1;
            product.CategoryId = 1;
            //Act
            var dbProduct = productRepo.GetById(5);

            //Expectation: dbProduct == product
            Assert.AreEqual(product.Id, dbProduct.Id);
            Assert.AreEqual(product.Name, dbProduct.Name);
            Assert.AreEqual(product.CreateDate, dbProduct.CreateDate);
            Assert.AreEqual(product.Price, dbProduct.Price);
            Assert.AreEqual(product.Status, dbProduct.Status);
            Assert.AreEqual(product.CategoryId, dbProduct.CategoryId);
        }
        [Test,Order(2)]
        public void GetByIdWithIdOutOfRange_GetNull()
        {
            //System.Threading.Thread.Sleep(2000);
            //Assign
            product.Id = 120;
            product.Name = "red bull";
            product.CreateDate = new DateTime(2022, 6, 13);
            product.Price = 12000;
            product.Status = 1;
            product.CategoryId = 1;

            Assert.AreEqual(null, productRepo.GetById(product.Id));

        }
        //[Test]
        //[TestCase(22,"Revive-a",18000)]
        //[TestCase(23, "Olong Tea-a",16000)]
        //public void CreateProductTest_Success(int id,string name,double price)
        //{
        //    //Assign
        //    //database id: int identity
        //    product.Id = id;
        //    product.Name = name;
        //    product.CreateDate = new DateTime(2022,6,21);
        //    product.Price = price;
        //    product.Status = 1;
        //    product.CategoryId = 1;
        //    //Act
        //    productRepo.Create(product);
        //    var dbProduct = productRepo.GetById(id);
        //    //Expectation 1: product is not null
        //    Assert.IsNotNull(dbProduct);
        //    //Expectation 2: dbProduct == product
        //    Assert.AreEqual(product.Id, dbProduct.Id);
        //    Assert.AreEqual(product.Name, dbProduct.Name);
        //    Assert.AreEqual(product.CreateDate, dbProduct.CreateDate);
        //    Assert.AreEqual(product.Price, dbProduct.Price);
        //    Assert.AreEqual(product.Status, dbProduct.Status);
        //    Assert.AreEqual(product.CategoryId, dbProduct.CategoryId);
        //}
        [Test]
        [TestCase("asdiaioshd iashdioashoiashdoiashdoiajsi iweeoqksmcn nhfh")]
        [TestCase("                      ")]
        public void CreateProductTest_NameRangeArgumentException(string name)
        {
            //System.Threading.Thread.Sleep(1000);
            product.Id = 14;
            product.Name = name;
            product.CreateDate = new DateTime(2022, 6, 13);
            product.Price = 15000;
            product.Status = 1;
            product.CategoryId = 1;

            var ex = Assert.Throws<ArgumentException>(() => productRepo.Create(product));
            Assert.That("The name must have [1,50] characters", Is.EqualTo(ex.Message));
        }
        [Test]
        [TestCase("1123123")]
        [TestCase("!@!@$%^^&*&*(*&*%$^%*_)(+_)")]
        public void CreateProductTest_NameSpecialCharacterException(string name)
        {
            char[] invalidCharacters = "`~!@#$%^&*()_+=0123456789<>,.?/\\|{}[]'\"".ToCharArray();
            Boolean valid = true;
            string s = "2022/01/11";

            DateTime dt = new DateTime();
            dt = DateTime.Parse(s);
            Product pro = new Product()
            {
                Name = name,
                Price = 1000,
                CreateDate = dt,
                Status = 1,
                CategoryId = 1,

            };
            if (pro.Name.IndexOfAny(invalidCharacters) >= 0)
            {

                var argumentException = Assert.Throws<ArgumentException>(() => productRepo.Create(pro));
                Assert.That("The name has invalid characters", Is.EqualTo(argumentException.Message));
            }
        }
        //[Test]
        //[TestCase("red bull strike")]
        //[TestCase("revive preserved lemon")]
        //public void GetByNameWithNameNotFound_GetNull(string name)
        //{
        //    //Assign
        //    product.Id = 12;
        //    product.Name = name;
        //    product.CreateDate = new DateTime(2022, 6, 13);
        //    product.Price = 12000;
        //    product.Status = 1;
        //    product.CategoryId = 1;

        //    Assert.AreEqual(null, productRepo.GetByName(product.Name));
        //}
        //[Test]
        //[TestCase(5, "Olong Tea Plus", "2022/6/8", 12000, 1, 1), Order(1)]
        ////[TestCase] need more test cases here.
        //public void GetByNameTest_GetProduct(int id, string name, DateTime dt, double price, int sta, int ctid)
        //{
        //    //Assign
        //    product.Id = id;
        //    product.Name = name;
        //    product.CreateDate = dt;
        //    product.Price = price;
        //    product.Status = sta;
        //    product.CategoryId = ctid;
        //    //Act
        //    var dbProduct = productRepo.GetByName("a p");

        //    //Expectation: dbProduct == product
        //    Assert.AreEqual(product.Id, dbProduct.Id);
        //    Assert.AreEqual(product.Name, dbProduct.Name);
        //    Assert.AreEqual(product.CreateDate, dbProduct.CreateDate);
        //    Assert.AreEqual(product.Price, dbProduct.Price);
        //    Assert.AreEqual(product.Status, dbProduct.Status);
        //    Assert.AreEqual(product.CategoryId, dbProduct.CategoryId);
        //}

        [Test, Order(3)]
        public void Test_3seconds()
        {
            //System.Threading.Thread.Sleep(3000);
        }
    }
    //[TestFixture]
    //[Parallelizable]
    //public class TestParallel
    //{
    //    [Test]
    //    public void TimeConsumingTest()
    //    {
    //        System.Threading.Thread.Sleep(4000);
    //    }
    //}
}

