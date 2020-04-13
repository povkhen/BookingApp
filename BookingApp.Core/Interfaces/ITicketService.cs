using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingApp.Core.Interfaces
{
    public interface ITicketService
    {
        Task<Guid?> NewCustomer(string firstName, string lastName, string typecustomer);
        Task<IEnumerable<string>> GetAllTypeCustomers();
    }
}
