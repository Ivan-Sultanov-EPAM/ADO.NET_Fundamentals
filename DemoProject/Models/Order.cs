using System;
using System.Globalization;

namespace DemoProject.Models
{
    public class Order
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int ProductId { get; set; }

        public new void ToString()
        {
            Console.WriteLine(Id.ToString());
            Console.WriteLine(Status.ToString());
            Console.WriteLine(CreatedDate.ToString(CultureInfo.InvariantCulture));
            Console.WriteLine(UpdatedDate.ToString(CultureInfo.InvariantCulture));
            Console.WriteLine(ProductId.ToString());
            Console.WriteLine();
        }
    }
}