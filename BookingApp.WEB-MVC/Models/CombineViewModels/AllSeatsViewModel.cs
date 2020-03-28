using Newtonsoft.Json;
using System.Collections.Generic;

namespace BookingApp.WEB_MVC.Models
{
    public class AllSeatsViewModel<T> where T : class
    {
        [JsonProperty(PropertyName = "Trip")]
        public TripViewModel<SearchTripViewModel> Trip { get; set; }

        [JsonProperty(PropertyName = "Cars")]
        public IEnumerable<IEnumerable<T>> Cars { get; set; }

        [JsonProperty(PropertyName = "CarInfo")]
        public CarInfo CarInfo { get; set; }
    }
}