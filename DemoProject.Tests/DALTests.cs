using DemoProject.Models;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace DemoProject.Tests
{
    public class DALTests : TestBase
    {
        [Fact]
        public void Should_Add_Product()
        {
            var product = DataSource.Products[0];

            Dal.AddProduct(product).Should().BeTrue();

            Dal.GetAllProducts().Should()
                .BeEquivalentTo(new List<Product> { product },
                    config => config
                        .Excluding(p => p.Id));
        }

        [Fact]
        public void Should_Get_All_Products()
        {
            var product1 = DataSource.Products[0];
            var product2 = DataSource.Products[1];

            Dal.AddProduct(product1);
            Dal.AddProduct(product2);

            Dal.GetAllProducts().Should()
                .BeEquivalentTo(new List<Product> { product1, product2 },
                    config => config
                        .Excluding(p => p.Id));
        }

        [Fact]
        public void Should_Update_Product()
        {
            var product1 = DataSource.Products[0];
            var product2 = DataSource.Products[1];
            product2.Id = 1;

            Dal.AddProduct(product1);
            Dal.UpdateProduct(product2);

            Dal.GetAllProducts().Should()
                .BeEquivalentTo(new List<Product> { product2 });
        }
    }
}
