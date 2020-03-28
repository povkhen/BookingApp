using BookingApp.WEB_MVC.Models.Binding;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BookingApp.WEB_MVC.Models
{
    public class TripViewModel<T> where T: class
    {

        [JsonProperty(PropertyName = "Models")]
        public IEnumerable<T> Models { get; set; }

        [JsonProperty(PropertyName = "Bind")]
        public MainSearchBind Bind { get; set; }

    }
}