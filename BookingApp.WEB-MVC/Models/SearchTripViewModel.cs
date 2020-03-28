using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookingApp.WEB_MVC.Models
{
    public class SearchTripViewModel
    {
        [JsonProperty(PropertyName = "Id")]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "Train")]
        [Display(Name = "Train")]
        public string Train { get; set; }

        [JsonProperty(PropertyName = "Route")]
        [Display(Name = "Route")]
        public string Route { get; set; }

        [JsonProperty(PropertyName = "DepartureTime")]
        [Display(Name = "Departure Date/\nArrival Date")]
        public DateTime DepartureTime { get; set; }

        [JsonProperty(PropertyName = "ArrivalTime")]
        [Display(Name = "Departure time/\nArrival time")]
        public DateTime ArrivalTime { get; set; }

        [JsonProperty(PropertyName = "Duration")]
        [Display(Name = "Duration")]
        public string Duration { get; set; }

        [JsonProperty(PropertyName = "FreeSeats")]
        [Display(Name = "Free Seats")]
        public IEnumerable<SeatSearchViewModel> FreeSeats { get; set; }

    }
}