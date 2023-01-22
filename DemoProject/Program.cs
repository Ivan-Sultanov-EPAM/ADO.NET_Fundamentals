namespace DemoProject
{
    internal class Program
    {
        public static string ConnectionString = "Data Source=:memory:;Version=3;New=True;";
        //public static string ConnectionString2 = "Data Source=(localdb)\\MSSQLLocalDB;Database = AdoNetFundamentalsDb;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False";

        public static void Main(string[] args)
        {
            //CreateDB();
        }

        //public static void CreateDB()
        //{
        //    var connectionString = "Data Source=:memory:;Version=3;New=True;";

        //    using var connection = new SQLiteConnection(connectionString);
        //    connection.CreateDbWithData();

        //    var dal = new DAL(connection);

        //    dal.AddProduct(new Product
        //    {
        //        Name = "OnePlus11",
        //        Description = "Smartphone",
        //        Weight = 215,
        //        Height = 10,
        //        Width = 75,
        //        Length = 176
        //    });

        //    var test = dal.UpdateProduct(new Product
        //    {
        //        Id = 1,
        //        Name = "Vacuum",
        //        Description = "Vacuum Robot",
        //        Weight = 3,
        //        Height = 8,
        //        Width = 50,
        //        Length = 50
        //    });

        //    var products = dal.GetAllProducts();


        //    foreach (var product in products)
        //    {
        //        product.ToString();
        //    }

        //    dal.AddOrder(new Order
        //    {
        //        Status = OrderStatus.NotStarted,
        //        CreatedDate = new DateTime(2023, 1, 12),
        //        UpdatedDate = new DateTime(2023, 1, 12),
        //        ProductId = 6
        //    });

        //    var orders = dal.GetAllOrders();

        //    foreach (var order in orders)
        //    {
        //        order.ToString();
        //    }

        //    dal.GetProductById(2).ToString();
        //    dal.GetOrderById(2).ToString();
        //}
    }
}