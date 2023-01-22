using DemoProject.Helpers;
using DemoProject.Models;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace DemoProject
{
    public class DAL
    {
        private readonly SQLiteConnection _connection;

        public DAL(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public bool AddProduct(Product product)
        {
            using var cmd = new SQLiteCommand();
            cmd.Connection = _connection;

            cmd.CommandText = "INSERT INTO products (name, description, weight, height, width, length) values " +
                              $"('{product.Name}'," +
                              $"'{product.Description}'," +
                              $"{product.Weight}," +
                              $"{product.Height}," +
                              $"{product.Width}," +
                              $"{product.Length})";

            return cmd.ExecuteNonQuery() == 1;
        }

        public Product GetProductById(int id)
        {
            var products = new List<Product>();

            using var cmd = new SQLiteCommand();
            cmd.Connection = _connection;

            cmd.CommandText = $"SELECT * FROM products WHERE id = {id};";

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
            return products.SingleOrDefault();
        }

        public bool UpdateProduct(Product product)
        {
            using var cmd = new SQLiteCommand();
            cmd.Connection = _connection;

            cmd.CommandText = "UPDATE products SET " +
                              $"name = '{product.Name}'," +
                              $"description = '{product.Description}'," +
                              $"weight = {product.Weight}," +
                              $"height = {product.Height}," +
                              $"width = {product.Width}," +
                              $"length = {product.Length} WHERE id = {product.Id}";

            return cmd.ExecuteNonQuery() == 1;
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

        public bool AddOrder(Order order)
        {
            using var cmd = new SQLiteCommand();
            cmd.Connection = _connection;

            cmd.CommandText = "INSERT INTO orders (status, created_date, updated_date, product_id) values " +
                              $"('{order.Status}'," +
                              $"'{order.CreatedDate}'," +
                              $"'{order.UpdatedDate}'," +
                              $"{order.ProductId})";

            return cmd.ExecuteNonQuery() == 1;
        }

        public Order GetOrderById(int id)
        {
            var orders = new List<Order>();

            using var cmd = new SQLiteCommand();
            cmd.Connection = _connection;

            cmd.CommandText = $"SELECT * FROM orders WHERE id = {id};";

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
            return orders.SingleOrDefault();
        }

        public bool UpdateOrder(Order order)
        {
            using var cmd = new SQLiteCommand();
            cmd.Connection = _connection;

            cmd.CommandText = "UPDATE orders SET" +
                              $"status = '{order.Status}'," +
                              $"created_date = '{order.CreatedDate}'," +
                              $"updated_date = '{order.UpdatedDate}'," +
                              $"product_id = {order.ProductId} WHERE id = {order.Id}";

            return cmd.ExecuteNonQuery() == 1;
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