using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DemoProject.Helpers;
using DemoProject.Models;

namespace DemoProject
{
    public class DAL
    {
        private readonly SqlConnection _connection;

        public DAL(SqlConnection connection)
        {
            _connection = connection;
        }

        public bool AddProduct(Product product)
        {
            using var cmd = new SqlCommand();
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

            using var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = $"SELECT * FROM products WHERE id = {id};";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
                products.Add(new Product
                {
                    Id = reader["id"].ToInt(),
                    Name = reader["name"].ToString(),
                    Description = reader["description"].ToString(),
                    Weight = reader["weight"].ToDecimal(),
                    Height = reader["height"].ToDecimal(),
                    Width = reader["width"].ToDecimal(),
                    Length = reader["length"].ToDecimal()
                });

            reader.Close();
            return products.SingleOrDefault();
        }

        public bool UpdateProduct(Product product)
        {
            using var cmd = new SqlCommand();
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

        public bool DeleteProduct(int id)
        {
            using var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = $"DELETE FROM products WHERE id = {id}";

            return cmd.ExecuteNonQuery() == 1;
        }

        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();

            using var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = "select * from products;";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
                products.Add(new Product
                {
                    Id = reader["id"].ToInt(),
                    Name = reader["name"].ToString(),
                    Description = reader["description"].ToString(),
                    Weight = reader["weight"].ToDecimal(),
                    Height = reader["height"].ToDecimal(),
                    Width = reader["width"].ToDecimal(),
                    Length = reader["length"].ToDecimal()
                });

            reader.Close();
            return products;
        }

        public bool AddOrder(Order order)
        {
            using var cmd = new SqlCommand();
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

            using var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = $"SELECT * FROM orders WHERE id = {id};";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
                orders.Add(new Order
                {
                    Id = reader["id"].ToInt(),
                    Status = reader["status"].ToOrderStatus(),
                    CreatedDate = reader["created_date"].ToDate(),
                    UpdatedDate = reader["updated_date"].ToDate(),
                    ProductId = reader["product_id"].ToInt()
                });

            reader.Close();
            return orders.SingleOrDefault();
        }

        public bool UpdateOrder(Order order)
        {
            using var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = "UPDATE orders SET " +
                              $"status = '{order.Status}'," +
                              $"created_date = '{order.CreatedDate}'," +
                              $"updated_date = '{order.UpdatedDate}'," +
                              $"product_id = {order.ProductId} WHERE id = {order.Id}";

            return cmd.ExecuteNonQuery() == 1;
        }

        public bool DeleteOrder(int id)
        {
            using var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = $"DELETE FROM orders WHERE id = {id}";

            return cmd.ExecuteNonQuery() == 1;
        }

        public List<Order> GetAllOrders()
        {
            var orders = new List<Order>();

            using var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = "select * from orders;";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
                orders.Add(new Order
                {
                    Id = reader["id"].ToInt(),
                    Status = reader["status"].ToOrderStatus(),
                    CreatedDate = reader["created_date"].ToDate(),
                    UpdatedDate = reader["updated_date"].ToDate(),
                    ProductId = reader["product_id"].ToInt()
                });

            reader.Close();
            return orders;
        }

        public List<Order> GetFilteredOrders(
            int? year = null,
            int? month = null,
            OrderStatus? status = null,
            int? product = null)
        {
            var orders = new List<Order>();

            using var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spGetFilteredOrders";

            cmd.Parameters.Add("@Year", SqlDbType.Int).Value = year;
            cmd.Parameters.Add("@Month", SqlDbType.Int).Value = month;
            cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = status;
            cmd.Parameters.Add("@Product", SqlDbType.Int).Value = product;

            var reader = cmd.ExecuteReader();

            while (reader.Read())
                orders.Add(new Order
                {
                    Id = reader["id"].ToInt(),
                    Status = reader["status"].ToOrderStatus(),
                    CreatedDate = reader["created_date"].ToDate(),
                    UpdatedDate = reader["updated_date"].ToDate(),
                    ProductId = reader["product_id"].ToInt()
                });

            reader.Close();
            return orders;
        }

        public void ClearAllData()
        {
            using var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = "EXEC spClearDb;";
            cmd.ExecuteNonQuery();
        }
    }
}