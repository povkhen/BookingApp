using System;

namespace BookingApp.Data.Entities.Procedure_Models
{
    public partial class TripSearch
    {
        public Guid Id { get; set; }

        public string Train { get; set; }
        public string Route { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string Duration { get; set; }
        
        public virtual Trip Trip { get; set; }
        
        public virtual Train TrainObj { get; set; }

        public virtual Route RouteObj { get; set; }
    }
}
