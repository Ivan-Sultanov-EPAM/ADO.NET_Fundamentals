using System;

namespace DemoProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public decimal Length { get; set; }

        public new void ToString()
        {
            Console.WriteLine(Id.ToString());
            Console.WriteLine(Name);
            Console.WriteLine(Description);
            Console.WriteLine(Weight.ToString());
            Console.WriteLine(Height.ToString());
            Console.WriteLine(Width.ToString());
            Console.WriteLine(Length.ToString());
            Console.WriteLine();
        }
    }
}