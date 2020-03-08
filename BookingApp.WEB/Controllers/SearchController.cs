using AutoMapper;
using BookingApp.Core.DTO;
using BookingApp.Core.Interfaces;
using BookingApp.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BookingApp.WEB.Controllers
{
    public class SearchController : Controller
    {
        private readonly IRouteService _service;
        public SearchController(IRouteService service)
        {
            _service = service;
        }
        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<ActionResult> About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public async Task<ActionResult> Search(int page = 1)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TripSearchDTO, SearchTripViewModel>()).CreateMapper();
            IEnumerable<TripSearchDTO> tripsDTOs = await _service.SearchTrip("Львів","Рівне",DateTime.Parse("2020-03-30"));
            var trips = mapper.Map<IEnumerable<TripSearchDTO>, List<SearchTripViewModel>>(tripsDTOs);
            
            int pageSize = 3; 
            IEnumerable<SearchTripViewModel> phonesPerPages = trips.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = trips.Count };
            IndexViewModel<SearchTripViewModel> ivm = new IndexViewModel<SearchTripViewModel>{ PageInfo = pageInfo, Models = phonesPerPages };
            return View(ivm);
        }
    }
}