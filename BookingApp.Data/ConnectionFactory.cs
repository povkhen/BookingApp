using System.Data;
using System.Data.SqlClient;

namespace BookingApp.Data
{
    public static class ConnectionFactory
    {
        public static SqlConnection SqlConnection()
        {
            string connectionString = "Data Source=MEO;Initial Catalog=BookingDB;Integrated Security=True";
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
