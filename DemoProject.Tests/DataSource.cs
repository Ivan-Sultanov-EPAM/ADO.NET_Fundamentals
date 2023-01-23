using DemoProject.Models;
using System;
using System.Collections.Generic;

namespace DemoProject.Tests
{
    internal static class DataSource
    {
        public static readonly List<Product> Products = new List<Product>
        {
            new Product
            {
                Name = "OnePlus11",
                Description = "Smartphone",
                Weight = 215,
                Height = 10,
                Width = 75,
                Length = 176
            },
            new Product
            {
                Name = "Lenovo",
                Description = "Laptop",
                Weight = 3,
                Height = 25,
                Width = 65,
                Length = 55
            }
        };

        public static readonly List<Order> Orders = new List<Order>
        {
            new Order
            {
                Status = OrderStatus.NotStarted,
                CreatedDate = new DateTime(2022, 12, 7),
                UpdatedDate = new DateTime(2022, 12, 7),
                ProductId = 1
            },
            new Order
            {
                Status = OrderStatus.Done,
                CreatedDate = new DateTime(2023, 1, 17),
                UpdatedDate = new DateTime(2023, 2, 25),
                ProductId = 2
            }
        };
    }
}