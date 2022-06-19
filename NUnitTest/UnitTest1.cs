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
        public void GetByIdTest_GetProduct()
        {
            //Assign
            product.Id = 12;
            product.Name = "red bull";
            product.CreateDate = new DateTime(2022, 6, 13);
            product.Price = 12000;
            product.Status = 1;
            product.CategoryId = 1;
            //Act
            var dbProduct = productRepo.GetById(12);

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
        public void CreateProductTest_Success()
        {
            //Assign
            //database id: int identity
            product.Id = 24;
            product.Name = "revive lemon salt";
            product.CreateDate = new DateTime(2022, 6, 13);
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
        public void CreateProductTest_NameRangeArgumentException()
        {
            //Assign
            //database id: int identity
            product.Id = 14;
            product.Name = "          ";
            product.CreateDate = new DateTime(2022, 6, 13);
            product.Price = 15000;
            product.Status = 1;
            product.CategoryId = 1;

            var ex = Assert.Throws<ArgumentException>(() => productRepo.Create(product));
            Assert.That(ex.Message, Is.EqualTo("The name must have [1,50] characterss"));

            product.Name = "asdiaioshd iashdioashoiashdoiashdoia haoisdhaoishdaosihdaisohd aoisdhaoi";
            ex = Assert.Throws<ArgumentException>(() => productRepo.Create(product));
            Assert.That(ex.Message, Is.EqualTo("The name must have [1,50] characterss"));
        }
        [Test]
        public void CreateProductTest_DateOutOfRange()
        {
            //Assign
            //database id: int identity
            product = new Product();
            product.Id = 14;
            product.Name = "cafe lon";
            product.CreateDate = new DateTime(2022, 6, 18);
            product.Price = 15000;
            product.Status = 1;
            product.CategoryId = 1;

            var ex = Assert.Throws<ArgumentException>(() => productRepo.Create(product));
            Assert.That(ex.Message, Is.EqualTo("The date must not exceed today"));
        }
        [Test]
        public void CreateProductTest_NameSpecialCharacterException()
        {
            char[] invalidCharacters = "`~!@#$%^&*()_+=0123456789<>,.?/\\|{}[]'\"".ToCharArray();
            Boolean valid = true;
            string s = "2022/01/11";

            DateTime dt = new DateTime();
            dt = DateTime.Parse(s);
            Product pro = new Product()
            {
                Name = "`~!@#$%^&*()_+=0123456789<>",
                Price = 1000,
                CreateDate = dt,
                Status = 1,
                CategoryId = 1,

            };
            if (pro.Name.IndexOfAny(invalidCharacters) >= 0)
            {

                var argumentException = Assert.Throws<ArgumentException>(() => productRepo.Create(pro));
                Assert.That(argumentException.Message, Is.EqualTo("Invalid characters"));
            }
        }
        [Test]
        public void GetByNameWithNameNotFound_GetNull()
        {
            //Assign
            product.Id = 12;
            product.Name = "red bull Strike";
            product.CreateDate = new DateTime(2022, 6, 13);
            product.Price = 12000;
            product.Status = 1;
            product.CategoryId = 1;

            Assert.AreEqual(null, productRepo.GetByName(product.Name));
        }

        [Test]
        public void GetByNameWithNameHasInvalidCharacters_GetException()
        {
            char[] invalidCharacters = "`~!@#$%^&*()_+=0123456789<>,.?/\\|{}[]'\"".ToCharArray();
            Boolean valid = true;
            string s = "2022/01/11";

            DateTime dt = new DateTime();
            dt = DateTime.Parse(s);
            Product pro = new Product()
            {
                Name = "`~!@#$%^&*()_+=0123456789<>",
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
        public void GetByName_NameRangeArgumentException()
        {
            //Assign
            //database id: int identity
            product.Id = 14;
            product.Name = "          ";
            product.CreateDate = new DateTime(2022, 6, 13);
            product.Price = 15000;
            product.Status = 1;
            product.CategoryId = 1;

            var ex = Assert.Throws<ArgumentException>(() => productRepo.GetByName(product.Name));
            Assert.That(ex.Message, Is.EqualTo("The name must have [1,50] characters"));

            product.Name = "asdiaioshd iashdioashoiashdoiashdoia haoisdhaoishdaosihdaisohd aoisdhaoi";
            ex = Assert.Throws<ArgumentException>(() => productRepo.GetByName(product.Name));
            Assert.That(ex.Message, Is.EqualTo("The name must have [1,50] characters"));
        }
        [Test]
        public void GetByNameTest_GetProduct()
        {
            //Assign
            product.Id = 12;
            product.Name = "red bull";
            product.CreateDate = new DateTime(2022, 6, 13);
            product.Price = 12000;
            product.Status = 1;
            product.CategoryId = 1;
            //Act
            var dbProduct = productRepo.GetByName("red bull");

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