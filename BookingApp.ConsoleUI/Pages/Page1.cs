using BookingApp.Core.Interfaces;
using BookingApp.Core.Services;
using BookingApp.Data.Infrastructure;
using EasyConsole;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ConsoleUI.Pages
{
    class Page1 : Page
    {
        private readonly IRouteService _service; 
        public Page1(Program program)
           : base("Всі можливі поїзди", program )
        {
            _service = new RouteService(new DalSession(), new Data.Infrastructure.Context());
            Console.InputEncoding = Encoding.Unicode;
   
        }
        public override void Display()
        {
            base.Display();
            Show().Wait();
        }
        public async Task Show()
        {
            string depart = Input.ReadString("Введіть станцію відправлення: ");

            if (!await _service.ExistStationByName(depart))
            {
                Output.WriteLine(ConsoleColor.Red, $"Не знайдено станцію: {depart}");
                await Task.Delay(3000);
                Program.NavigateHome();
            }

            string arrival = Input.ReadString("Введіть станцію прибуття: ");
            if (!await _service.ExistStationByName(arrival))
            {
                Output.WriteLine(ConsoleColor.Red, $"Не знайдено станцію: {arrival}");
                await Task.Delay(3000);
                Program.NavigateHome();
            }

            string datestring = Input.ReadString("Введіть дату в форматі (yyyy-MM-dd): ");
            if (DateTime.TryParse(datestring, out DateTime date)) { }
            else
            {
                Output.WriteLine(ConsoleColor.Red, "Не вірний ввід ");
                await Task.Delay(3000);
                Program.NavigateHome();
            }

            var trips = await _service.SearchTrip(depart, arrival, date);
            //var names = await _service.GetAllTypesCarName();
            
            if (trips.ToList().Count == 0)
            {
                Output.WriteLine(ConsoleColor.Red, $"Жодних поїздок не знайдено");
                await Task.Delay(3000);
                Program.NavigateHome();              
            }
                       
            Output.WriteLine(ConsoleColor.Cyan,String.Format("\n|{0,-10}|{1,-28}|{2,-25}|{3,-25}|{4,-13}|{5,-5}", "Поїзд", "Маршрут", "Станція відправлення", "Станція прибуття", "Тривалість", "Вагони"));
            foreach (var trip in trips)
            {
                var freeSeats = await _service.SearchFreeSeatById(trip.Id, depart, arrival);
                var freeSeatsString = "";
                foreach (var freeSeat in freeSeats)
                {
                    freeSeatsString += freeSeat.Car + "-" + freeSeat.Count + "  ";
                    //freeSeatsString += (freeSeats as IDictionary<string, object>)[propName].ToString() + "      ";
                }

                Output.WriteLine(ConsoleColor.White, "|{0,-10}|{1,-28}|{2,-25}|{3,-25}|{4,-13}|{5, -5}",
                    trip.Train, trip.Route, trip.DepartureTime, trip.ArrivalTime, trip.Duration, freeSeatsString);
            }
            Input.ReadString("Press [Enter] to navigate main page");
            Program.NavigateHome();



        }
    }
}
