using DemoProject.Models;
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
    }
}