using System;
using System.Collections.Generic;
using System.Text;

namespace BookingApp.Core.DTO
{
    public class TripSearchDTO
    {
        public Guid Id { get; set; }
        public string Train { get; set; }
        public string Route { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string Duration { get; set; }
    }
}
