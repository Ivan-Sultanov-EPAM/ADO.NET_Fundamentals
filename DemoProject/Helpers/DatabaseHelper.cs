using System.Data.SqlClient;
using System.Data.SQLite;

namespace DemoProject.Helpers
{
    public static class DatabaseHelper
    {
        public static void CreateDbWithData(this SqlConnection connection)
        {
            connection.Open();

            using var cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = "CREATE TABLE products (" +
                              "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                              "name VARCHAR(20)," +
                              "description VARCHAR(20)," +
                              "weight INTEGER," +
                              "height INTEGER," +
                              "width INTEGER," +
                              "length INTEGER)";

            cmd.ExecuteNonQuery();

            //cmd.CommandText = "INSERT INTO products (name, description, weight, height, width, length) values " +
            //                  "('Laptop1', 'Gaming Laptop1', 2, 3, 50, 40)," +
            //                  "('Laptop2', 'Gaming Laptop2', 3, 3, 50, 40)," +
            //                  "('Laptop3', 'Gaming Laptop3', 4, 3, 50, 40)," +
            //                  "('Laptop4', 'Gaming Laptop4', 5, 3, 50, 40)," +
            //                  "('Laptop5', 'Gaming Laptop5', 6, 3, 50, 40);";

            //cmd.ExecuteNonQuery();

            cmd.CommandText = "CREATE TABLE orders (" +
                              "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                              "status VARCHAR(20)," +
                              "created_date TEXT," +
                              "updated_date TEXT," +
                              "product_id INTEGER," +
                              "FOREIGN KEY (product_id) " +
                              "REFERENCES products (id) " +
                              "ON DELETE CASCADE " +
                              "ON UPDATE NO ACTION)";

            cmd.ExecuteNonQuery();

            //cmd.CommandText = "INSERT INTO orders (status, created_date, updated_date, product_id) values " +
            //                  "('NotStarted', '2023-01-10', '2023-01-11', 3)";

            //cmd.ExecuteNonQuery();
        }
    }
}