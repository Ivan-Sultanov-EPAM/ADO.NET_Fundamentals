using System;
using System.Data.SqlClient;

namespace DemoProject.Tests
{
    public class TestBase : IDisposable
    {
        private readonly string _connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Database = AdoNetFundamentalsDb;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False";

        public readonly SqlConnection Connection;
        public readonly DAL Dal;

        public TestBase()
        {
            Connection = new SqlConnection(_connectionString);
            Connection.Open();
            Dal = new DAL(Connection);
            Dal.ClearAllData();
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}