using System;
using System.Data.SqlClient;

namespace DemoProject.Tests
{
    public class TestBase : IDisposable
    {
        private readonly string _connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Database = AdoNetFundamentalsDb;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=5;Encrypt=False;TrustServerCertificate=False";

        public readonly SqlConnection Connection;
        public readonly DalConnected DalConnected;
        public readonly DalDisconnected DalDisconnected;

        public TestBase()
        {
            Connection = new SqlConnection(_connectionString);
            Connection.Open();

            DalConnected = new DalConnected(Connection);
            DalDisconnected = new DalDisconnected(Connection);
        }

        public void Dispose()
        {
            DalConnected.ClearAllData();
            Connection.Dispose();
        }
    }
}