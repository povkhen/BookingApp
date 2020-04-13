using BookingApp.Core.Interfaces;
using BookingApp.Data.Infrastructure;
using BookingApp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Core.Services
{
    public class TicketService : ITicketService
    {
        private readonly IContext _context;
        public TicketService(IContext context)
        {
            _context = context;
        }

        public async Task<Guid?> NewCustomer(string firstName, string lastName, string typecustomer)
        {
            using (IDALSession _dalSession = new DalSession())
            {
                UnitOfWork unitOfWork = _dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    var id = await _context.StoredProcedures.NewCustomer(firstName, lastName, typecustomer);
                    unitOfWork.Commit();
                    return await Task.FromResult(id);

                }
                catch (KeyNotFoundException)
                {
                    unitOfWork.Rollback();
                    return null;
                }
            }
        }

        public async Task<IEnumerable<string>> GetAllTypeCustomers()
        {
            using (IDALSession _dalSession = new DalSession())
            {
                UnitOfWork unitOfWork = _dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    var alltypes = await _context.TypeCustomerRepo.GetAllAsync();
                    var names = alltypes.Select(x => x.Name);
                    unitOfWork.Commit();
                    return await Task.FromResult(names);

                }
                catch (KeyNotFoundException)
                {
                    unitOfWork.Rollback();
                    return null;
                }
            }
        }

        public async Task<Guid?> GetAll(string firstName, string lastName, string typecustomer)
        {
            using (IDALSession _dalSession = new DalSession())
            {
                UnitOfWork unitOfWork = _dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    var id = await _context.StoredProcedures.NewCustomer(firstName, lastName, typecustomer);
                    unitOfWork.Commit();
                    return await Task.FromResult(id);

                }
                catch (KeyNotFoundException)
                {
                    unitOfWork.Rollback();
                    return null;
                }
            }
        }


    }
}
