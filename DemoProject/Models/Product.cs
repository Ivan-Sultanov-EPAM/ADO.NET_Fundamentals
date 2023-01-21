using System;

namespace DemoProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }

        public new void ToString()
        {
            Console.WriteLine(Id.ToString());
            Console.WriteLine(Name.ToString());
            Console.WriteLine(Description.ToString());
            Console.WriteLine(Weight.ToString());
            Console.WriteLine(Height.ToString());
            Console.WriteLine(Width.ToString());
            Console.WriteLine(Length.ToString());
            Console.WriteLine();
        }
    }
}