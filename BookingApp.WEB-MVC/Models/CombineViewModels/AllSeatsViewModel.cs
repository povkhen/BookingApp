using System.Collections.Generic;

namespace BookingApp.WEB_MVC.Models
{
    public class AllSeatsViewModel<T> where T : class
    {
        public TripViewModel<SearchTripViewModel> Trip { get; set; }
        public IEnumerable<T> AllSeats { get; set; }
    }
}