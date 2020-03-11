using System;
using System.Collections.Generic;

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
        public IEnumerable<TypeCarSeatsDTO> FreeSeats { get; set; }
    }
}
