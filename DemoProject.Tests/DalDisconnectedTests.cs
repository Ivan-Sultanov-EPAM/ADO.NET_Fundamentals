using System.Collections.Generic;
using DemoProject.Models;
using FluentAssertions;
using Xunit;

namespace DemoProject.Tests
{
    public partial class DalTests
    {
        [Fact]
        public void Should_Add_Product_Using_Disconnected_Model()
        {
            var product1 = DataSource.Products[0];
            var product2 = DataSource.Products[0];

            DalDisconnected.GetAllProducts().Should()
                .BeEquivalentTo(new List<Product>());

            DalDisconnected.AddProduct(product1).Should().BeTrue();
            DalDisconnected.AddProduct(product2).Should().BeTrue();

            DalDisconnected.GetAllProducts().Should()
                .BeEquivalentTo(new List<Product> { product1, product2 },
                    config => config
                        .Excluding(p => p.Id));
        }

        [Fact]
        public void Should_Get_All_Products_Using_Disconnected_Model()
        {
            var product1 = DataSource.Products[0];
            var product2 = DataSource.Products[1];

            DalDisconnected.AddProduct(product1);
            DalDisconnected.AddProduct(product2);

            DalDisconnected.GetAllProducts().Should()
                .BeEquivalentTo(new List<Product> { product1, product2 },
                    config => config
                        .Excluding(p => p.Id));
        }

        [Fact]
        public void Should_Update_Product_Using_Disconnected_Model()
        {
            var product1 = DataSource.Products[0];
            var product2 = DataSource.Products[1];
            product2.Id = 1;

            DalDisconnected.AddProduct(product1);
            DalDisconnected.UpdateProduct(product2);

            DalDisconnected.GetAllProducts().Should()
                .BeEquivalentTo(new List<Product> { product2 });
        }

        [Fact]
        public void Should_Get_Product_By_Id_Using_Disconnected_Model()
        {
            var product1 = DataSource.Products[0];
            var product2 = DataSource.Products[1];

            DalDisconnected.AddProduct(product1);
            DalDisconnected.AddProduct(product2);

            DalDisconnected.GetProductById(1).Should()
                .BeEquivalentTo(product1, config =>
                    config.Excluding(p => p.Id));

            DalDisconnected.GetProductById(2).Should()
                .BeEquivalentTo(product2, config =>
                    config.Excluding(p => p.Id));
        }

        [Fact]
        public void Should_Delete_Product_Using_Disconnected_Model()
        {
            var product1 = DataSource.Products[0];
            var product2 = DataSource.Products[1];

            DalDisconnected.AddProduct(product1);
            DalDisconnected.AddProduct(product2);
            
            DalDisconnected.DeleteProduct(1).Should().BeTrue();

            DalDisconnected.GetAllProducts().Should()
                .BeEquivalentTo(new List<Product> { product2 },
                    config => config
                        .Excluding(p => p.Id));
        }

        [Fact]
        public void Should_Add_Order_Using_Disconnected_Model()
        {
            AddProductsDisconnectedModel();

            var order = DataSource.Orders[0];

            DalDisconnected.AddOrder(order).Should().BeTrue();

            DalDisconnected.GetAllOrders().Should()
                .BeEquivalentTo(new List<Order> { order },
                    config => config
                        .Excluding(p => p.Id));
        }

        [Fact]
        public void Should_Get_All_Orders_Using_Disconnected_Model()
        {
            AddProductsDisconnectedModel();

            var order1 = DataSource.Orders[0];
            var order2 = DataSource.Orders[1];

            DalDisconnected.AddOrder(order1);
            DalDisconnected.AddOrder(order2);

            DalDisconnected.GetAllOrders().Should()
                .BeEquivalentTo(new List<Order> { order1, order2 },
                    config => config
                        .Excluding(p => p.Id));
        }

        [Fact]
        public void Should_Update_Order_Using_Disconnected_Model()
        {
            AddProductsDisconnectedModel();

            var order1 = DataSource.Orders[0];
            var order2 = DataSource.Orders[1];
            DalDisconnected.AddOrder(order1);
            DalDisconnected.AddOrder(order2);

            order1.Id = 1;
            order1.Status = OrderStatus.InProgress;
            order1.UpdatedDate = order1.UpdatedDate.AddDays(10);

            DalDisconnected.UpdateOrder(order1);

            DalDisconnected.GetAllOrders().Should()
                .BeEquivalentTo(new List<Order> { order1, order2 },
                    config => config
                        .Excluding(o => o.Id));
        }

        [Fact]
        public void Should_Get_Order_By_Id_Using_Disconnected_Model()
        {
            AddProductsDisconnectedModel();

            var order1 = DataSource.Orders[0];
            var order2 = DataSource.Orders[1];
            DalDisconnected.AddOrder(order1);
            DalDisconnected.AddOrder(order2);

            DalDisconnected.GetOrderById(1).Should()
                .BeEquivalentTo(order1, config =>
                    config.Excluding(p => p.Id));

            DalDisconnected.GetOrderById(2).Should()
                .BeEquivalentTo(order2, config =>
                    config.Excluding(p => p.Id));
        }

        [Fact]
        public void Should_Delete_Order_Using_Disconnected_Model()
        {
            AddProductsDisconnectedModel();

            var order1 = DataSource.Orders[0];
            var order2 = DataSource.Orders[1];
            DalDisconnected.AddOrder(order1);
            DalDisconnected.AddOrder(order2);

            DalDisconnected.DeleteOrder(1).Should().BeTrue();

            DalDisconnected.GetAllOrders().Should()
                .BeEquivalentTo(new List<Order> { order2 },
                    config => config
                        .Excluding(p => p.Id));
        }

        [Theory]
        [MemberData(nameof(GetFilteredOrdersTestData))]
        public void Should_Get_Filtered_Orders_Using_Disconnected_Model(
            int? year,
            int? month,
            OrderStatus? status,
            int? product,
            List<Order> expected)
        {
            AddProductsDisconnectedModel();

            DalDisconnected.AddOrder(DataSource.Orders[0]);
            DalDisconnected.AddOrder(DataSource.Orders[1]);
            DalDisconnected.AddOrder(DataSource.Orders[2]);
            DalDisconnected.AddOrder(DataSource.Orders[3]);

            var result = DalDisconnected.GetFilteredOrders(
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
        public void Should_Delete_Orders_Using_Disconnected_Model(
            int? year,
            int? month,
            OrderStatus? status,
            int? product,
            List<Order> expected)
        {
            AddProductsConnectedModel();

            DalDisconnected.AddOrder(DataSource.Orders[0]);
            DalDisconnected.AddOrder(DataSource.Orders[1]);
            DalDisconnected.AddOrder(DataSource.Orders[2]);
            DalDisconnected.AddOrder(DataSource.Orders[3]);

            DalDisconnected.DeleteOrders(
                year: year,
                month: month,
                status: status,
                product: product
            );

            var result = DalDisconnected.GetAllOrders();

            result.Should()
                .BeEquivalentTo(expected, config => config
                    .Excluding(p => p.Id));
        }
    }
}