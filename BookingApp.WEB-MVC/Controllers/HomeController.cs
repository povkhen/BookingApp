using BookingApp.Core.DTO;
using BookingApp.Core.Interfaces;
using BookingApp.WEB_MVC.Models;

using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using BookingApp.WEB_MVC.Models.Binding;
using System.Linq;

namespace BookingApp.WEB_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapperTrip;
        private readonly IRouteService _service;
        public HomeController(IRouteService service)
        {
            _service = service;
            _mapperTrip = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TripSearchDTO, SearchTripViewModel>();
                cfg.CreateMap<TypeCarSeatsDTO, SeatSearchViewModel>();
            }).CreateMapper();

        }
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<ActionResult> About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public async Task<ActionResult> AutocompleteSearch(string term)
        {
            var stations = await _service.GetAllStations();
            var models = stations.Where(a => a.Name.Contains(term))
            .Select(a => new { value = a.Name })
            .Distinct();

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Booking(SearchTripViewModel item, SeatSearchViewModel item2)
        {
            List<SearchTripViewModel> trips = new List<SearchTripViewModel>();
            List<SeatSearchViewModel> car = new List<SeatSearchViewModel>();
            
            car.Add(item2);
            item.FreeSeats = car;
            trips.Add(item);
            
            TripViewModel<SearchTripViewModel> viewModel = new TripViewModel<SearchTripViewModel> { Models = trips };
            return View(viewModel);
        }

        public async Task<ActionResult> SearchPage()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Search(MainSearchBind bind)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<TripSearchDTO> tripsDTOs = await _service.SearchTrip(bind.From, bind.To, bind.Date);
      
                var trips = _mapperTrip.Map<IEnumerable<TripSearchDTO>, List<SearchTripViewModel>>(tripsDTOs);
                TripViewModel<SearchTripViewModel> res = new TripViewModel<SearchTripViewModel>{ Models = trips };
                return PartialView(res);
            }
            else
            {
                return View("SearchPage");
            }
        }


    }
}