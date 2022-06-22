using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement
{
    public class ProductRepository
    {
        char[] invalidCharacters = "`~!@#$%^&*()_+=0123456789<>,.?/\\|{}[]'\"".ToCharArray();
        string cs = @"Server=DESKTOP-HPMUS7S\SQLEXPRESS; Database=PRN_ProductDB1; User Id=sa; Password=thuytan123";
        SqlConnection conn;
        DateTime now = DateTime.Now;
        public void Create(Product entity)
        {
            if (entity.Name.Trim().Equals("") || entity.Name.Length > 50)
                throw new ArgumentException("The name must have [1,50] characters");
            if (entity.Name.IndexOfAny(invalidCharacters) >= 0)
                throw new ArgumentException("The name has invalid characters");
            if (entity.Price < 0)
                throw new ArgumentException("The price must larger than or equal 0");

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

        public Product GetByName(string Name)
        {
            try
            {
                if (Name.Trim().Equals("") || Name.Length > 50)
                    throw new ArgumentException("The name must have [1,50] characters");
                if (Name.IndexOfAny(invalidCharacters) >= 0)
                    throw new ArgumentException("The name has invalid characters");

                SqlConnection conn = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("select Id,Name,CreateDate,Price,Status,CategoryId from Product WHERE Name=@name AND Status=1", conn);
                cmd.Parameters.AddWithValue("@name", Name.ToLower());
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
