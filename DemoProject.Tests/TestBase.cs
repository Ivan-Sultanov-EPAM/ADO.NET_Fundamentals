using DemoProject.Helpers;
using System;
using System.Data.SQLite;

namespace DemoProject.Tests
{
    public class TestBase : IDisposable
    {
        private readonly string _connectionString = "Data Source=:memory:;Version=3;New=True;";
        public readonly SQLiteConnection Connection;
        public readonly DAL Dal;

        public TestBase()
        {
            Connection = new SQLiteConnection(_connectionString);
            Connection.CreateDbWithData();
            Dal = new DAL(Connection);
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}