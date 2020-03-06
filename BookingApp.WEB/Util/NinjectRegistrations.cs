using BookingApp.Core.ApplicationService;
using BookingApp.Core.DataService;
using BookingApp.Data;
using BookingApp.Core.Services;
using Ninject.Modules;

namespace BookingApp.WEB.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IRouteService>().To<RouteService>();
            Bind<IContext>().To<Context>();
        }
    }
}