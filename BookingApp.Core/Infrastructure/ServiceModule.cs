using BookingApp.Data.Infrastructure;
using BookingApp.Data.Interfaces;
using Ninject.Modules;

namespace BookingApp.Core.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IContext>().To<Context>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IDALSession>().To<DalSession>();
        }
    }
}
