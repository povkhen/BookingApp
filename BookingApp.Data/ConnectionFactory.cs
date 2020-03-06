using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BookingApp.Data
{
    public static class ConnectionFactory
    {
        public static SqlConnection SqlConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BookingDB"].ConnectionString;
            return new SqlConnection(connectionString);
        }
        public static IDbConnection CreateConnection()
        {
            var conn = SqlConnection();
            conn.Open();
            return conn;
        }
    }
}
