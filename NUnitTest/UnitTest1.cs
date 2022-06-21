using NUnit.Framework;
using ProductManagement;
using System;

namespace NUnitTest
{
    public class Tests
    {
        ProductRepository productRepo = new ProductRepository();
        Product product = new Product();

        [Test]
        [TestCase(5, "FrenchFries", "2022/8/10", 15000, 1, 1), Order(1)]
        public void GetByIdTest_GetProduct(int id, string name, DateTime dt, double price, int sta, int ctid)
        {
            //Assign
            product.Id = 4;
            product.Name = "Olong Tea";
            product.CreateDate = new DateTime(2022, 6, 19);
            product.Price = 12000;
            product.Status = 1;
            product.CategoryId = 1;
            //Act
            var dbProduct = productRepo.GetById(4);

            //Expectation: dbProduct == product
            Assert.AreEqual(product.Id, dbProduct.Id);
            Assert.AreEqual(product.Name, dbProduct.Name);
            Assert.AreEqual(product.CreateDate, dbProduct.CreateDate);
            Assert.AreEqual(product.Price, dbProduct.Price);
            Assert.AreEqual(product.Status, dbProduct.Status);
            Assert.AreEqual(product.CategoryId, dbProduct.CategoryId);
        }
        [Test]
        public void GetByIdWithIdOutOfRange_GetNull()
        {
            //Assign
            product.Id = 120;
            product.Name = "red bull";
            product.CreateDate = new DateTime(2022, 6, 13);
            product.Price = 12000;
            product.Status = 1;
            product.CategoryId = 1;

            Assert.AreEqual(null, productRepo.GetById(product.Id));

        }
        [Test]
        [TestCase("Olong tea"),Order(1)]
        [TestCase("Revive")]
        public void CreateProductTest_Success(string name)
        {
            //Assign
            //database id: int identity
            product.Id = 3;
            product.Name = name;
            product.CreateDate = new DateTime(2022, 6, 14);
            product.Price = 15000;
            product.Status = 1;
            product.CategoryId = 1;
            //Act
            productRepo.Create(product);
            int id = product.Id;
            var dbProduct = productRepo.GetById(id);
            //Expectation 1: product is not null
            Assert.IsNotNull(dbProduct);
            //Expectation 2: dbProduct == product
            Assert.AreEqual(product.Id, dbProduct.Id);
            Assert.AreEqual(product.Name, dbProduct.Name);
            Assert.AreEqual(product.CreateDate, dbProduct.CreateDate);
            Assert.AreEqual(product.Price, dbProduct.Price);
            Assert.AreEqual(product.Status, dbProduct.Status);
            Assert.AreEqual(product.CategoryId, dbProduct.CategoryId);
        }
        [Test]
        [TestCase("asdiaioshd iashdioashoiashdoiashdoia haoisdhaoishdaosihdaisohd aoisdhaoi"), Order(1)]
        [TestCase("                      ")]
        public void CreateProductTest_NameRangeArgumentException(string name)
        {
            //Assign
            //database id: int identity
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
        [TestCase("2022/1/1"), Order(1)]
        [TestCase("2022/2/2")]
        public void CreateProductTest_DateOutOfRange(DateTime dt)
        {
            //Assign
            //database id: int identity
            product = new Product();
            product.Id = 6;
            product.Name = "cafe lon";
            product.CreateDate = dt;
            product.Price = 15000;
            product.Status = 1;
            product.CategoryId = 1;

            var ex = Assert.Throws<ArgumentException>(() => productRepo.Create(product));
            Assert.That("The date must not exceed today", Is.EqualTo(ex.Message));
        }
        [Test]
        [TestCase("1123123"), Order(1)]
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
        [Test]
        [TestCase("red bull strike"), Order(1)]
        [TestCase("revive preserved lemon")]
        public void GetByNameWithNameNotFound_GetNull(string name)
        {
            //Assign
            product.Id = 12;
            product.Name = name;
            product.CreateDate = new DateTime(2022, 6, 13);
            product.Price = 12000;
            product.Status = 1;
            product.CategoryId = 1;

            Assert.AreEqual(null, productRepo.GetByName(product.Name));
        }

        [Test]
        [TestCase("1123123"), Order(1)]
        [TestCase("!@!@$%^^&*&*(*&*%$^%*_)(+_)")]
        public void GetByNameWithNameHasInvalidCharacters_GetException(string name)
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
                var argumentException = Assert.Throws<ArgumentException>(() => productRepo.GetByName(pro.Name));
                Assert.That(argumentException.Message, Is.EqualTo("The name has invalid characters"));
            }
        }

        [Test]
        [TestCase("asdiaioshd iashdioashoiashdoiashdoia haoisdhaoishdaosihdaisohd aoisdhaoi"), Order(1)]
        [TestCase("                      ")]
        public void GetByName_NameRangeArgumentException(string name)
        {
            //Assign
            //database id: int identity
            product.Id = 14;
            product.Name = name;
            product.CreateDate = new DateTime(2022, 6, 13);
            product.Price = 15000;
            product.Status = 1;
            product.CategoryId = 1;

            var ex = Assert.Throws<ArgumentException>(() => productRepo.GetByName(product.Name));
            Assert.That(ex.Message, Is.EqualTo("The name must have [1,50] characters"));
        }
        [Test]
        [TestCase(5,"Olong Tea Plus", "2022/6/8", 12000, 1, 1), Order(1)]
        //[TestCase] need more test cases here.
        public void GetByNameTest_GetProduct(int id, string name, DateTime dt, double price, int sta, int ctid)
        {
            //Assign
            product.Id = id;
            product.Name = name;
            product.CreateDate = dt;
            product.Price = price;
            product.Status = sta;
            product.CategoryId = ctid;
            //Act
            var dbProduct = productRepo.GetByName("a p");

            //Expectation: dbProduct == product
            Assert.AreEqual(product.Id, dbProduct.Id);
            Assert.AreEqual(product.Name, dbProduct.Name);
            Assert.AreEqual(product.CreateDate, dbProduct.CreateDate);
            Assert.AreEqual(product.Price, dbProduct.Price);
            Assert.AreEqual(product.Status, dbProduct.Status);
            Assert.AreEqual(product.CategoryId, dbProduct.CategoryId);
        }
    }
}