using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProductManagement
{
    public class ProductRepository : IRepository<Product>
    {
        Regex regex = new Regex(@"[0-9?=,.:;-\\s]+");
        string cs=@"Server=DESKTOP-HPMUS7S\SQLEXPRESS; Database=PRN_ProductDB; User Id=sa; Password=thuytan123";
        SqlConnection conn;
        public void Create(Product entity)
        {
            if (entity.Name.Trim().Equals("") || entity.Name.Length > 50)
                throw new ArgumentException("The name must have [1,50] characters");
            //if (Match.Success)
            //    throw new ArgumentException("The name must not have special characters");
            try
            {
                conn = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("INSERT INTO Product(Name,CreateDate,Price,Status,CategoryId) VALUES (@Name,@CreateDate,@Price,1,@CategoryId)", conn);
                cmd.Parameters.AddWithValue("@Name", entity.Name);
                cmd.Parameters.AddWithValue("@CreateDate", entity.CreateDate);
                cmd.Parameters.AddWithValue("@Price", entity.Price);
                cmd.Parameters.AddWithValue("@CategoryId", entity.CategoryId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        public Product GetById(int id)
        {
            try
            {
                SqlConnection conn = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("select Id,Name,CreateDate,Price,Status,CategoryId from Product WHERE Id=@id AND Status=1", conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                var rs = cmd.ExecuteReader();
                rs.Read();
                if (rs.HasRows)
                {
                    Product Pd = new Product()
                    {
                        Id = (int)rs["Id"],
                        Name = (string)rs["Name"],
                        CreateDate = (DateTime)rs["CreateDate"],
                        Price = (double)rs["Price"],
                        Status = (int)rs["Status"],
                        CategoryId = (int)rs["CategoryId"]
                    };
                    if (Pd != null)
                        return Pd;
                }
                conn.Close();
                return null;

            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
