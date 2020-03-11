using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BookingApp.WEB_MVC.Models
{
    public class SearchTripViewModel
    {

        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [Display(Name = "Train")]
        public string Train { get; set; }
 
        [Display(Name = "Route")]
        public string Route { get; set; }

        [Display(Name = "Departure Date\nArrival Date")]
        public DateTime DepartureTime { get; set; }
        
        [Display(Name = "Departure time\nArrival time")]
        public DateTime ArrivalTime { get; set; }

        [Display(Name = "Duration")]
        public string Duration { get; set; }

        [Display(Name = "Free Seats")]
        public IEnumerable<SeatSearchViewModel> FreeSeats { get; set; }
        
    }
}