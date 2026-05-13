using System.Data.SqlClient;

namespace FolklorApp
{
    public static class DatabaseHelper
    {
        // Promeni connection string prema vašem SQL Server-u
        private static string connectionString =
            @"Server=.\SQLEXPRESS;Database=Folklor;Trusted_Connection=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static string ConnectionString
        {
            get => connectionString;
            set => connectionString = value;
        }
    }
}
