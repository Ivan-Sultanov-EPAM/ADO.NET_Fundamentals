using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DemoProject.Helpers;
using DemoProject.Models;

namespace DemoProject
{
    public class DalDisconnected
    {
        private readonly SqlConnection _connection;

        private readonly SqlDataAdapter _productDataAdapter;
        private readonly DataSet _productDataSet;
        private readonly DataTable _productTable;

        private readonly SqlDataAdapter _orderDataAdapter;
        private readonly DataSet _orderDataSet;
        private readonly DataTable _orderTable;

        public DalDisconnected(SqlConnection connection)
        {
            _connection = connection;

            _productDataAdapter = new SqlDataAdapter("select * from products;", _connection);
            _productDataSet = new DataSet();
            _productDataAdapter.FillSchema(_productDataSet, SchemaType.Source, "Products");
            _productDataAdapter.Fill(_productDataSet, "Products");
            _productDataSet.Tables["Products"].Columns["id"].AutoIncrement = true;
            _productDataSet.Tables["Products"].Columns["id"].AutoIncrementSeed = 1;
            _productTable = _productDataSet.Tables["Products"];

            _orderDataAdapter = new SqlDataAdapter("select * from orders;", _connection);
            _orderDataSet = new DataSet();
            _orderDataAdapter.FillSchema(_orderDataSet, SchemaType.Source, "Orders");
            _orderDataAdapter.Fill(_orderDataSet, "Orders");
            _orderDataSet.Tables["Orders"].Columns["id"].AutoIncrement = true;
            _orderDataSet.Tables["Orders"].Columns["id"].AutoIncrementSeed = 1;
            _orderTable = _orderDataSet.Tables["Orders"];
        }

        public bool AddProduct(Product product)
        {
            var newProduct = _productDataSet.Tables["Products"].NewRow();

            newProduct["name"] = product.Name;
            newProduct["description"] = product.Description;
            newProduct["weight"] = product.Weight;
            newProduct["height"] = product.Height;
            newProduct["width"] = product.Width;
            newProduct["length"] = product.Length;

            _productTable.Rows.Add(newProduct);
            
            _ = new SqlCommandBuilder(_productDataAdapter);
            
            return _productDataAdapter.Update(_productDataSet, "Products") == 1;
        }

        public Product GetProductById(int id)
        {
            var row = _productTable
                .AsEnumerable()
                .FirstOrDefault(x => x.Field<int>("id") == id);

            var product = new Product
            {
                Id = row["id"].ToInt(),
                Name = row["name"].ToString(),
                Description = row["description"].ToString(),
                Weight = row["weight"].ToDecimal(),
                Height = row["height"].ToDecimal(),
                Width = row["width"].ToDecimal(),
                Length = row["length"].ToDecimal()
            };

            return product;
        }

        public bool UpdateProduct(Product product)
        {
            var productToUpdate = _productTable.Rows.Find(product.Id);

            productToUpdate["name"] = product.Name;
            productToUpdate["description"] = product.Description;
            productToUpdate["weight"] = product.Weight;
            productToUpdate["height"] = product.Height;
            productToUpdate["width"] = product.Width;
            productToUpdate["length"] = product.Length;

            _ = new SqlCommandBuilder(_productDataAdapter);

            return _productDataAdapter.Update(_productDataSet, "Products") == 1;
        }

        public bool DeleteProduct(int id)
        {
            _productTable.Rows.Find(id).Delete();

            _ = new SqlCommandBuilder(_productDataAdapter);

            return _productDataAdapter.Update(_productDataSet, "Products") == 1;
        }

        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();

            foreach (DataRow row in _productTable.Rows)
            {
                products.Add(new Product
                {
                    Id = row["id"].ToInt(),
                    Name = row["name"].ToString(),
                    Description = row["description"].ToString(),
                    Weight = row["weight"].ToDecimal(),
                    Height = row["height"].ToDecimal(),
                    Width = row["width"].ToDecimal(),
                    Length = row["length"].ToDecimal()
                });
            }
            
            return products;
        }

        public bool AddOrder(Order order)
        {
            var newOrder = _orderDataSet.Tables["Orders"].NewRow();

            newOrder["status"] = order.Status;
            newOrder["created_date"] = order.CreatedDate;
            newOrder["updated_date"] = order.UpdatedDate;
            newOrder["product_id"] = order.ProductId;

            _orderTable.Rows.Add(newOrder);

            _ = new SqlCommandBuilder(_orderDataAdapter);

            return _orderDataAdapter.Update(_orderDataSet, "Orders") == 1;
        }

        public Order GetOrderById(int id)
        {
            var row = _orderTable
                .AsEnumerable()
                .FirstOrDefault(x => x.Field<int>("id") == id);

            var order = new Order
            {
                Id = row["id"].ToInt(),
                Status = row["status"].ToOrderStatus(),
                CreatedDate = row["created_date"].ToDate(),
                UpdatedDate = row["updated_date"].ToDate(),
                ProductId = row["product_id"].ToInt()
            };

            return order;
        }

        public bool UpdateOrder(Order order)
        {
            var orderToUpdate = _orderTable.Rows.Find(order.Id);

            orderToUpdate["status"] = order.Status;
            orderToUpdate["created_date"] = order.CreatedDate;
            orderToUpdate["updated_date"] = order.UpdatedDate;
            orderToUpdate["product_id"] = order.ProductId;

            _ = new SqlCommandBuilder(_orderDataAdapter);

            return _orderDataAdapter.Update(_orderDataSet, "Orders") == 1;
        }

        public bool DeleteOrder(int id)
        {
            _orderTable.Rows.Find(id).Delete();

            _ = new SqlCommandBuilder(_orderDataAdapter);

            return _orderDataAdapter.Update(_orderDataSet, "Orders") == 1;
        }

        public List<Order> GetAllOrders()
        {
            var orders = new List<Order>();

            foreach (DataRow row in _orderTable.Rows)
            {
                orders.Add(new Order
                {
                    Id = row["id"].ToInt(),
                    Status = row["status"].ToOrderStatus(),
                    CreatedDate = row["created_date"].ToDate(),
                    UpdatedDate = row["updated_date"].ToDate(),
                    ProductId = row["product_id"].ToInt()
                });
            }

            return orders;
        }

        public List<Order> GetFilteredOrders(
            int? year = null,
            int? month = null,
            OrderStatus? status = null,
            int? product = null)
        {
            var orders = new List<Order>();

            var rows = _orderTable.AsEnumerable();

            if (year != null)
            {
                rows = rows.Where(x =>
                    x.Field<DateTime>("created_date").Year == year);
            }

            if (month != null)
            {
                rows = rows.Where(x =>
                    x.Field<DateTime>("created_date").Month == month);
            }

            if (status != null)
            {
                rows = rows.Where(x =>
                    x.Field<string>("status") == status.ToString());
            }

            if (product != null)
            {
                rows = rows.Where(x =>
                    x.Field<int>("product_id") == product);
            }

            foreach (DataRow row in rows)
            {
                orders.Add(new Order
                {
                    Id = row["id"].ToInt(),
                    Status = row["status"].ToOrderStatus(),
                    CreatedDate = row["created_date"].ToDate(),
                    UpdatedDate = row["updated_date"].ToDate(),
                    ProductId = row["product_id"].ToInt()
                });
            }

            return orders;
        }

        public void DeleteOrders(
            int? year = null,
            int? month = null,
            OrderStatus? status = null,
            int? product = null)
        {
            var rows = _orderTable.AsEnumerable();

            if (year != null)
            {
                rows = rows.Where(x =>
                    x.Field<DateTime>("created_date").Year == year);
            }

            if (month != null)
            {
                rows = rows.Where(x =>
                    x.Field<DateTime>("created_date").Month == month);
            }

            if (status != null)
            {
                rows = rows.Where(x =>
                    x.Field<string>("status") == status.ToString());
            }

            if (product != null)
            {
                rows = rows.Where(x =>
                    x.Field<int>("product_id") == product);
            }

            foreach (DataRow row in rows)
            {
                row.Delete();
            }

            _ = new SqlCommandBuilder(_orderDataAdapter);

            _orderDataAdapter.Update(_orderDataSet, "Orders");
        }
    }
}