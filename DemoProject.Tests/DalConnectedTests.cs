using System.Collections.Generic;
using DemoProject.Models;
using FluentAssertions;
using Xunit;

namespace DemoProject.Tests
{
    public partial class DalTests : TestBase
    {
        [Fact]
        public void Should_Add_Product_Using_Connected_Model()
        {
            var product = DataSource.Products[0];

            DalConnected.AddProduct(product).Should().BeTrue();

            DalConnected.GetAllProducts().Should()
                .BeEquivalentTo(new List<Product> { product },
                    config => config
                        .Excluding(p => p.Id));
        }

        [Fact]
        public void Should_Get_All_Products_Using_Connected_Model()
        {
            var product1 = DataSource.Products[0];
            var product2 = DataSource.Products[1];

            DalConnected.AddProduct(product1);
            DalConnected.AddProduct(product2);

            DalConnected.GetAllProducts().Should()
                .BeEquivalentTo(new List<Product> { product1, product2 },
                    config => config
                        .Excluding(p => p.Id));
        }

        [Fact]
        public void Should_Update_Product_Using_Connected_Model()
        {
            var product1 = DataSource.Products[0];
            var product2 = DataSource.Products[1];
            product2.Id = 1;

            DalConnected.AddProduct(product1);
            DalConnected.UpdateProduct(product2);

            DalConnected.GetAllProducts().Should()
                .BeEquivalentTo(new List<Product> { product2 });
        }

        [Fact]
        public void Should_Get_Product_By_Id_Using_Connected_Model()
        {
            var product1 = DataSource.Products[0];
            var product2 = DataSource.Products[1];

            DalConnected.AddProduct(product1);
            DalConnected.AddProduct(product2);

            DalConnected.GetProductById(1).Should()
                .BeEquivalentTo(product1, config =>
                    config.Excluding(p => p.Id));

            DalConnected.GetProductById(2).Should()
                .BeEquivalentTo(product2, config =>
                    config.Excluding(p => p.Id));
        }

        [Fact]
        public void Should_Delete_Product_Using_Connected_Model()
        {
            var product1 = DataSource.Products[0];
            var product2 = DataSource.Products[1];

            DalConnected.AddProduct(product1);
            DalConnected.AddProduct(product2);

            DalConnected.DeleteProduct(1).Should().BeTrue();

            DalConnected.GetAllProducts().Should()
                .BeEquivalentTo(new List<Product> { product2 },
                    config => config
                        .Excluding(p => p.Id));
        }

        [Fact]
        public void Should_Add_Order_Using_Connected_Model()
        {
            AddProductsConnectedModel();

            var order = DataSource.Orders[0];

            DalConnected.AddOrder(order).Should().BeTrue();

            DalConnected.GetAllOrders().Should()
                .BeEquivalentTo(new List<Order> { order },
                    config => config
                        .Excluding(p => p.Id));
        }

        [Fact]
        public void Should_Get_All_Orders_Using_Connected_Model()
        {
            AddProductsConnectedModel();

            var order1 = DataSource.Orders[0];
            var order2 = DataSource.Orders[1];

            DalConnected.AddOrder(order1);
            DalConnected.AddOrder(order2);

            DalConnected.GetAllOrders().Should()
                .BeEquivalentTo(new List<Order> { order1, order2 },
                    config => config
                        .Excluding(p => p.Id));
        }

        [Fact]
        public void Should_Update_Order_Using_Connected_Model()
        {
            AddProductsConnectedModel();

            var order1 = DataSource.Orders[0];
            var order2 = DataSource.Orders[1];
            DalConnected.AddOrder(order1);
            DalConnected.AddOrder(order2);

            order1.Id = 1;
            order1.Status = OrderStatus.InProgress;
            order1.UpdatedDate = order1.UpdatedDate.AddDays(10);

            DalConnected.UpdateOrder(order1);

            DalConnected.GetAllOrders().Should()
                .BeEquivalentTo(new List<Order> { order1, order2 },
                    config => config
                        .Excluding(o => o.Id));
        }

        [Fact]
        public void Should_Get_Order_By_Id_Using_Connected_Model()
        {
            AddProductsConnectedModel();

            var order1 = DataSource.Orders[0];
            var order2 = DataSource.Orders[1];
            DalConnected.AddOrder(order1);
            DalConnected.AddOrder(order2);

            DalConnected.GetOrderById(1).Should()
                .BeEquivalentTo(order1, config =>
                    config.Excluding(p => p.Id));

            DalConnected.GetOrderById(2).Should()
                .BeEquivalentTo(order2, config =>
                    config.Excluding(p => p.Id));
        }

        [Fact]
        public void Should_Delete_Order_Using_Connected_Model()
        {
            AddProductsConnectedModel();

            var order1 = DataSource.Orders[0];
            var order2 = DataSource.Orders[1];
            DalConnected.AddOrder(order1);
            DalConnected.AddOrder(order2);

            DalConnected.DeleteOrder(1).Should().BeTrue();

            DalConnected.GetAllOrders().Should()
                .BeEquivalentTo(new List<Order> { order2 },
                    config => config
                        .Excluding(p => p.Id));
        }

        [Theory]
        [MemberData(nameof(GetFilteredOrdersTestData))]
        public void Should_Get_Filtered_Orders_Using_Connected_Model(
            int? year,
            int? month,
            OrderStatus? status,
            int? product,
            List<Order> expected)
        {
            AddProductsConnectedModel();

            DalConnected.AddOrder(DataSource.Orders[0]);
            DalConnected.AddOrder(DataSource.Orders[1]);
            DalConnected.AddOrder(DataSource.Orders[2]);
            DalConnected.AddOrder(DataSource.Orders[3]);

            var result = DalConnected.GetFilteredOrders(
                year: year,
                month: month,
                status: status,
                product: product
                );

            result.Should()
                .BeEquivalentTo(expected, config => config
                         .Excluding(p => p.Id));
        }

        [Theory]
        [MemberData(nameof(DeleteOrdersTestData))]
        public void Should_Delete_Orders_Using_Connected_Model(
            int? year,
            int? month,
            OrderStatus? status,
            int? product,
            List<Order> expected)
        {
            AddProductsConnectedModel();

            DalConnected.AddOrder(DataSource.Orders[0]);
            DalConnected.AddOrder(DataSource.Orders[1]);
            DalConnected.AddOrder(DataSource.Orders[2]);
            DalConnected.AddOrder(DataSource.Orders[3]);

            DalConnected.DeleteOrders(
                year: year,
                month: month,
                status: status,
                product: product
            );

            var result = DalConnected.GetAllOrders();

            result.Should()
                .BeEquivalentTo(expected, config => config
                    .Excluding(p => p.Id));
        }

        private void AddProductsConnectedModel()
        {
            DalConnected.AddProduct(DataSource.Products[0]);
            DalConnected.AddProduct(DataSource.Products[1]);
        }

        private void AddProductsDisconnectedModel()
        {
            DalDisconnected.AddProduct(DataSource.Products[0]);
            DalDisconnected.AddProduct(DataSource.Products[1]);
        }

        public static IEnumerable<object[]> GetFilteredOrdersTestData()
        {
            var data = new List<object[]>
            {
                new object[] { null, null, null, null, new List<Order>
                {
                    DataSource.Orders[0],
                    DataSource.Orders[1],
                    DataSource.Orders[2],
                    DataSource.Orders[3]
                }},
                new object[] { 2023, null, null, null, new List<Order>
                {
                    DataSource.Orders[1]
                }},
                new object[] { null, 5, null, null, new List<Order>
                {
                    DataSource.Orders[2]
                }},
                new object[] { null, null, OrderStatus.Loading, null, new List<Order>
                {
                    DataSource.Orders[3]
                }},
                new object[] { null, null, null, 2, new List<Order>
                {
                    DataSource.Orders[1],
                    DataSource.Orders[3]
                }},
                new object[] { 2024, 1, null, null, new List<Order>
                {
                    DataSource.Orders[3]
                }},
                new object[] { null, 1, OrderStatus.Loading, null, new List<Order>
                {
                    DataSource.Orders[3]
                }},
                new object[] { 2023, 1, OrderStatus.Done, 2, new List<Order>
                {
                    DataSource.Orders[1]
                }}
            };

            return data;
        }

        public static IEnumerable<object[]> DeleteOrdersTestData()
        {
            var data = new List<object[]>
            {
                new object[] { null, null, null, null, new List<Order>()},
                new object[] { 2023, null, null, null, new List<Order>
                {
                    DataSource.Orders[0],
                    DataSource.Orders[2],
                    DataSource.Orders[3]
                }},
                new object[] { null, 5, null, null, new List<Order>
                {
                    DataSource.Orders[0],
                    DataSource.Orders[1],
                    DataSource.Orders[3]
                }},
                new object[] { null, null, OrderStatus.Loading, null, new List<Order>
                {
                    DataSource.Orders[0],
                    DataSource.Orders[1],
                    DataSource.Orders[2]
                }},
                new object[] { null, null, null, 2, new List<Order>
                {
                    DataSource.Orders[0],
                    DataSource.Orders[2]
                }},
                new object[] { 2024, 1, null, null, new List<Order>
                {
                    DataSource.Orders[0],
                    DataSource.Orders[1],
                    DataSource.Orders[2]
                }},
                new object[] { null, 1, OrderStatus.Loading, null, new List<Order>
                {
                    DataSource.Orders[0],
                    DataSource.Orders[1],
                    DataSource.Orders[2]
                }},
                new object[] { 2023, 1, OrderStatus.Done, 2, new List<Order>
                {
                    DataSource.Orders[0],
                    DataSource.Orders[2],
                    DataSource.Orders[3]
                }}
            };

            return data;
        }
    }
}