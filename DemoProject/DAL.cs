using DemoProject.Helpers;
using DemoProject.Models;
using System.Collections.Generic;
using System.Data.SQLite;

namespace DemoProject
{
    public class DAL
    {
        private readonly SQLiteConnection _connection;

        public DAL(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public void AddProduct(Product product)
        {
            using var cmd = new SQLiteCommand();
            cmd.Connection = _connection;

            cmd.CommandText = "insert into products (name, description, weight, height, width, length) values " +
                              $"('{product.Name}'," +
                              $"'{product.Description}'," +
                              $"{product.Weight}," +
                              $"{product.Height}," +
                              $"{product.Width}," +
                              $"{product.Length})";

            cmd.ExecuteNonQuery();
        }

        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();

            using var cmd = new SQLiteCommand();
            cmd.Connection = _connection;

            cmd.CommandText = "select * from products;";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                products.Add(new Product
                {
                    Id = reader["id"].ToInt(),
                    Name = reader["name"].ToString(),
                    Description = reader["description"].ToString(),
                    Weight = reader["weight"].ToInt(),
                    Height = reader["height"].ToInt(),
                    Width = reader["width"].ToInt(),
                    Length = reader["length"].ToInt()
                });
            }

            reader.Close();
            return products;
        }

        public void AddOrder(Order order)
        {
            using var cmd = new SQLiteCommand();
            cmd.Connection = _connection;

            cmd.CommandText = "insert into orders (status, created_date, updated_date, product_id) values " +
                              $"('{order.Status}'," +
                              $"'{order.CreatedDate}'," +
                              $"'{order.UpdatedDate}'," +
                              $"{order.ProductId})";

            cmd.ExecuteNonQuery();
        }

        public List<Order> GetAllOrders()
        {
            var orders = new List<Order>();

            using var cmd = new SQLiteCommand();
            cmd.Connection = _connection;

            cmd.CommandText = "select * from orders;";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                orders.Add(new Order
                {
                    Id = reader["id"].ToInt(),
                    Status = reader["status"].ToOrderStatus(),
                    CreatedDate = reader["created_date"].ToDate(),
                    UpdatedDate = reader["updated_date"].ToDate(),
                    ProductId = reader["product_id"].ToInt()
                });
            }

            reader.Close();
            return orders;
        }
    }
}