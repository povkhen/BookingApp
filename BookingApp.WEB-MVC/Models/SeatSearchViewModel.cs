using Newtonsoft.Json;

namespace BookingApp.WEB_MVC.Models
{
    public class SeatSearchViewModel
    {
        [JsonProperty(PropertyName = "Car")]
        public string Car { get; set; }

        [JsonProperty(PropertyName = "Count")]
        public string Count { get; set; }
    }
}