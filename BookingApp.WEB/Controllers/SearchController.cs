using AutoMapper;
using BookingApp.Core.DTO;
using BookingApp.Core.Interfaces;
using BookingApp.WEB.Models;
using System;
using System.Collections.Generic;
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

        public async Task<ActionResult> Search()
        {
            IEnumerable<TripSearchDTO> tripsDTOs = await _service.SearchTrip("Львів","Рівне",DateTime.Parse("2020-03-30"));
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TripSearchDTO, SearchTripViewModel>()).CreateMapper();
            var trips = mapper.Map<IEnumerable<TripSearchDTO>, List<SearchTripViewModel>>(tripsDTOs);
            return View(trips);
        }
    }
}