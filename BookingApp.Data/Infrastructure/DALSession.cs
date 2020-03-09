using System.Data;
using BookingApp.Data.Interfaces;

namespace BookingApp.Data.Infrastructure
{
    public sealed class DalSession : IDALSession
    {
        public DalSession()
        {
            _connection = DBConnection.SqlConnection();
            _connection.Open();
            UnitOfWork = new UnitOfWork(_connection);
        }

        readonly IDbConnection _connection = null;

        public UnitOfWork UnitOfWork { get; } = null;

        public void Dispose()
        {
            UnitOfWork.Dispose();
            _connection.Dispose();
        }
    }
}
