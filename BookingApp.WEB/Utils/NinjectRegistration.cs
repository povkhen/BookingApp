using BookingApp.Core.Interfaces;
using BookingApp.Core.Services;
using Ninject.Modules;

namespace BookingApp.WEB.Utils
{
    public class NinjectRegistration : NinjectModule
    {
        public override void Load()
        {
            Bind<IRouteService>().To<RouteService>();         
        }
    }
}
