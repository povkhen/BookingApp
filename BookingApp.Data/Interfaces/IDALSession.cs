using BookingApp.Data.Infrastructure;
using System;

namespace BookingApp.Data.Interfaces
{
    public interface IDALSession : IDisposable
    {
        UnitOfWork UnitOfWork { get; }
    }
}
