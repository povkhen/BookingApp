using System;

namespace BookingApp.WEB.Models
{
    public class SearchTripViewModel
    {
        public Guid Id { get; set; }
        public string Train { get; set; }
        public string Route { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string Duration { get; set; }
        
    }
}