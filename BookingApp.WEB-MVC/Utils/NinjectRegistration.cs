using BookingApp.Core.Interfaces;
using BookingApp.Core.Services;
using Ninject.Modules;

namespace BookingApp.WEB_MVC.Utils
{
    public class NinjectRegistration : NinjectModule
    {
        public override void Load()
        {
            Bind<IRouteService>().To<RouteService>();         
        }
    }
}
