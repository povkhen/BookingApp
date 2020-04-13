using AutoMapper;
using BookingApp.Core.DTO;
using BookingApp.Core.Interfaces;
using BookingApp.WEB_MVC.Models;
using BookingApp.WEB_MVC.Models.Binding;
using BookingApp.WEB_MVC.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BookingApp.WEB_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapperTrip;
        private readonly IRouteService _routeService;
        private readonly ICostService _costService;
        private readonly ITicketService _ticketService;
        public HomeController(IRouteService routeService, ICostService costService, ITicketService ticketService)
        {
            _routeService = routeService;
            _costService = costService;
            _ticketService = ticketService;
            _mapperTrip = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TripSearchDTO, SearchTripViewModel>();
                cfg.CreateMap<TypeCarSeatsDTO, SeatSearchViewModel>();
                cfg.CreateMap<AllrSeatsProcedureDTO, AllSeatsProcedureViewModel>();
            }).CreateMapper();

        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Submit()
        {
            ViewBag.Message = "Submit.";

            return View();
        }

        public async Task<ActionResult> AutocompleteSearch(string term)
        {
            var stations = await _routeService.GetAllStations();
            var models = stations.Where(a => a.Name.Contains(term))
            .Select(a => new { value = a.Name })
            .Distinct();

            return Json(models, JsonRequestBehavior.AllowGet);
        }


        public TripViewModel<SearchTripViewModel> GetTripViewModel(SearchTripViewModel item, SeatSearchViewModel item2, MainSearchBind bind)
        {
            List<SearchTripViewModel> trips = new List<SearchTripViewModel>();
            List<SeatSearchViewModel> car = new List<SeatSearchViewModel>();
            car.Add(item2);
            item.FreeSeats = car;
            trips.Add(item);
            return new TripViewModel<SearchTripViewModel> { Models = trips, Bind = bind };
        }

        [HttpPost]
        public async Task<ActionResult> CustomerForm(SearchTripViewModel dummy,  MainSearchBind bind, Guid[] selectedObjects)
        {
            if (Request.IsAjaxRequest() && selectedObjects.Length != 0)
            {
                List<TicketBind> tickets = new List<TicketBind>();
            
                foreach (Guid guid in selectedObjects)
                {
                    tickets.Add(
                        new TicketBind
                        {
                            TripId = dummy.Id,
                            SeatId = guid,
                            ArrivalStationId = await _routeService.GetIdStation(bind.To),
                            DepartureStationId = await _routeService.GetIdStation(bind.From),
                            ArrivalTime = dummy.ArrivalTime,
                            DepartureTime = dummy.DepartureTime,
                            //TODO:
                            Price = "123"
                        }
                    );
                }
                return PartialView(tickets);
                }
            
            else return null;
        }


        [HttpPost]
        public async Task<ActionResult> Booking(SearchTripViewModel item, SeatSearchViewModel item2, MainSearchBind bind)
        {
            IEnumerable<AllrSeatsProcedureDTO> allrSeatsDto = await _routeService.SearchAllSeatById(item.Id, bind.From, bind.To, item2.Car);

            var allseats = _mapperTrip.Map<IEnumerable<AllrSeatsProcedureDTO>, List<AllSeatsProcedureViewModel>>(allrSeatsDto);

            foreach (var seat in allseats)
            {
                seat.Price = await _costService.GetCostOfSeatAsync(seat.PriceCoeff, item.Duration, (int)item.ArrivalTime.DayOfWeek);
                seat.SalePrice = await _costService.GetSaleCostOfSeatAsync(seat.PriceCoeff, item.Duration, (int)item.ArrivalTime.DayOfWeek);
            }

            var cars = allseats
                        .GroupBy(u => u.CarNumber)
                        .Select(grp => grp.ToList())
                        .ToList();

            AllSeatsViewModel<AllSeatsProcedureViewModel> viewModel =
                    new AllSeatsViewModel<AllSeatsProcedureViewModel>
                    {
                        Trip = GetTripViewModel(item, item2, bind),
                        Cars = cars
                    };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Car(IEnumerable<AllSeatsProcedureViewModel> allseats)
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView(allseats);
            }
            else return null;
        }

        
        public ActionResult SearchPage()
        {
            return View();
        }
        

        [HttpPost]
        public async Task<ActionResult> Search(MainSearchBind bind)
        {
            if (ModelState.IsValid)
            {
                DateTime time = DateTime.ParseExact(bind.StartTime, "hh:mm tt", CultureInfo.InvariantCulture);
                IEnumerable<TripSearchDTO> tripsDTOs = await _routeService.SearchTrip(bind.From, bind.To, bind.Date);
                var trips = _mapperTrip.Map<IEnumerable<TripSearchDTO>, List<SearchTripViewModel>>(tripsDTOs)
                    .Where(x => x.DepartureTime.TimeOfDay >= time.TimeOfDay);
                TripViewModel<SearchTripViewModel> res = new TripViewModel<SearchTripViewModel> { Models = trips, Bind = bind };
                return PartialView(res);
            }
            else
            {
                return null;
            }
        }


    }
}