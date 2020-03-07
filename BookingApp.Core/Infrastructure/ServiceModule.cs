using BookingApp.Core.Interfaces;
using BookingApp.Core.Services;
using BookingApp.Data;
using BookingApp.Data.Interfaces;
using Ninject.Modules;

namespace BookingApp.Core.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}
