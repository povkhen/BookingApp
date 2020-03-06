using BookingApp.Core.ApplicationService;
using BookingApp.Core.DataService;
using BookingApp.Core.Services;
using EasyConsole;
using System;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ConsoleUI.Pages
{
    class Page2 : Page
    {
        private readonly IRouteService _service;
        public Page2(Program program)
           : base("Детальна інформація про маршрут", program)
        {
            _service = new RouteService((IContext)new Context());
            Console.InputEncoding = Encoding.Unicode;
        }
        public override void Display()
        {
            base.Display();
            Show().Wait();
        }

        public async Task Show()
        {
            string route = Input.ReadString("Введіть назву маршруту: ");

            if (!await _service.ExistRouteByName(route))
            {
                Output.WriteLine(ConsoleColor.Red, $"Не знайдено маршрут: {route}");
                await Task.Delay(3000);
                Program.NavigateHome();
            }

            var stations = await _service.GetRouteInfo(route);

            Output.WriteLine(ConsoleColor.Cyan, String.Format("\n|{0,-20}|{1,-10}", "Порядковий номер", "Станція"));
            foreach (var station in stations)
            {
                Output.WriteLine(ConsoleColor.White, "|{0,-20}|{1,-10}", station.SequenceNumber, station.Name);
            }
            Input.ReadString("Press [Enter] to navigate main page");
            Program.NavigateHome();

        }
    }
}
