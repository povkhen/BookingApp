using BookingApp.Core.DTO;
using BookingApp.Core.Interfaces;
using BookingApp.WEB_MVC.Models;

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BookingApp.WEB_MVC.Models.Binding;

namespace BookingApp.WEB_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRouteService _service;
        public HomeController(IRouteService service)
        {
            _service = service;
        }
        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<ActionResult> Booking(Guid id, string type)
        {
            ViewBag.TripId = id.ToString();
            ViewBag.TypeCar = type;
            return View();
        }

        public async Task<ActionResult> About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public async Task<ActionResult> SearchPage()
        {
            var stations = await _service.GetAllStationsName();
            return View(stations);
        }


        [HttpPost]
        public async Task<ActionResult> Search(MainSearchBind bind, int page = 1)
        {           
            IMapper mapperTrip = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TripSearchDTO, SearchTripViewModel>();
                cfg.CreateMap<TypeCarSeatsDTO, SeatSearchViewModel>();
            }).CreateMapper();
            
            IEnumerable<TripSearchDTO> tripsDTOs = await _service.SearchTrip(bind.From, bind.To, bind.Date);
      
            var trips = mapperTrip.Map<IEnumerable<TripSearchDTO>, List<SearchTripViewModel>>(tripsDTOs);
            
            int pageSize = 3; 
            IEnumerable<SearchTripViewModel> tripsPerPages = trips.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = trips.Count };
            IndexViewModel<SearchTripViewModel> ivm = new IndexViewModel<SearchTripViewModel>{ PageInfo = pageInfo, Models = tripsPerPages };
            return PartialView(ivm);
        }


    }
}