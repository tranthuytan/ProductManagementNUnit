using System;

namespace ProductManagement
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public double Price { get; set; }
        public int Status { get; set; }
        public int CategoryId { get; set; }
        public Product()
        {
            Id = 0;
            Name = "";
            CreateDate = default(DateTime);
            Price = 0;
            Status = 0;
            CategoryId = 0;
        }
        public Product(int id, string name, DateTime createDate, double price, int status, int categoryId)
        {
            Id = id;
            Name = name;
            CreateDate = createDate;
            Price = price;
            Status = status;
            CategoryId = categoryId;
        }
    }
}
