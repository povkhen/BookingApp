using BookingApp.Core.DataService;
using BookingApp.Core.Entities;
using BookingApp.Static.Data.Repositories;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingApp.Data.Repositories
{
    public class RouteRepository : Repository<Route>, IRouteRepository
    {
        public RouteRepository(string tableName) : base(tableName) { }

        public async Task<Route> FindRouteByNameAsync(string name)
        {
            using (var connection = ConnectionFactory.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<Route>($"SELECT * FROM {_tableName} WHERE Name=@Name", new { Name = name });
                if (result == null)
                    throw new KeyNotFoundException($"{_tableName} with name [{name}] could not be found.");
                return result;
            }
        }
    }
}
