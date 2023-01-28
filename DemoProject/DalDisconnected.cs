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
        private readonly DataSet _dataSet;

        private readonly SqlDataAdapter _productDataAdapter;
        private readonly DataTable _productTable;

        private readonly SqlDataAdapter _orderDataAdapter;
        private readonly DataTable _orderTable;

        public DalDisconnected(SqlConnection connection)
        {
            _dataSet = new DataSet();
            _productDataAdapter = new SqlDataAdapter("select * from products;", connection);
            
            _productDataAdapter.FillSchema(_dataSet, SchemaType.Source, "Products");
            _productDataAdapter.Fill(_dataSet, "Products");
            _dataSet.Tables["Products"].Columns["id"].AutoIncrement = true;
            _dataSet.Tables["Products"].Columns["id"].AutoIncrementSeed = 1;
            _productTable = _dataSet.Tables["Products"];

            _orderDataAdapter = new SqlDataAdapter("select * from orders;", connection);
            _orderDataAdapter.FillSchema(_dataSet, SchemaType.Source, "Orders");
            _orderDataAdapter.Fill(_dataSet, "Orders");
            _dataSet.Tables["Orders"].Columns["id"].AutoIncrement = true;
            _dataSet.Tables["Orders"].Columns["id"].AutoIncrementSeed = 1;
            _orderTable = _dataSet.Tables["Orders"];

            var ordersProductsFk = new ForeignKeyConstraint("OrdersProductsFK",
                _dataSet.Tables["Products"].Columns["id"],
                _dataSet.Tables["Orders"].Columns["product_id"]);
            ordersProductsFk.DeleteRule = Rule.None;
            _dataSet.Tables["Orders"].Constraints.Add(ordersProductsFk);

            _ = new SqlCommandBuilder(_orderDataAdapter);
            _ = new SqlCommandBuilder(_productDataAdapter);
        }

        public bool AddProduct(Product product)
        {
            var newProduct = _dataSet.Tables["Products"].NewRow();

            newProduct["name"] = product.Name;
            newProduct["description"] = product.Description;
            newProduct["weight"] = product.Weight;
            newProduct["height"] = product.Height;
            newProduct["width"] = product.Width;
            newProduct["length"] = product.Length;

            _productTable.Rows.Add(newProduct);

            return _productDataAdapter.Update(_dataSet, "Products") == 1;
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

            return _productDataAdapter.Update(_dataSet, "Products") == 1;
        }

        public bool DeleteProduct(int id)
        {
            _productTable.Rows.Find(id).Delete();

            return _productDataAdapter.Update(_dataSet, "Products") == 1;
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
            var newOrder = _dataSet.Tables["Orders"].NewRow();

            newOrder["status"] = order.Status;
            newOrder["created_date"] = order.CreatedDate;
            newOrder["updated_date"] = order.UpdatedDate;
            newOrder["product_id"] = order.ProductId;

            _orderTable.Rows.Add(newOrder);

            return _orderDataAdapter.Update(_dataSet, "Orders") == 1;
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

            return _orderDataAdapter.Update(_dataSet, "Orders") == 1;
        }

        public bool DeleteOrder(int id)
        {
            _orderTable.Rows.Find(id).Delete();

            return _orderDataAdapter.Update(_dataSet, "Orders") == 1;
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

        public bool DeleteOrders(
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

            var numOfRowsToDelete = rows.Count();

            foreach (var row in rows)
            {
                row.Delete();
            }

            return _orderDataAdapter.Update(_dataSet, "Orders") == numOfRowsToDelete;
        }
    }
}