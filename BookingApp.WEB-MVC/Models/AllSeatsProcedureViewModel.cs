using Newtonsoft.Json;
using System;

namespace BookingApp.WEB_MVC.Models
{
    public partial class AllSeatsProcedureViewModel
    {
        [JsonProperty(PropertyName = "SeatId")]
        public Guid SeatId { get; set; }

        [JsonProperty(PropertyName = "CarNumber")]
        public int CarNumber { get; set; }

        [JsonProperty(PropertyName = "CarType")]
        public string CarType { get; set; }

        [JsonProperty(PropertyName = "SeatNumber")]
        public int SeatNumber { get; set; }

        [JsonProperty(PropertyName = "PriceCoeff")]
        public float PriceCoeff { get; set; }

        [JsonProperty(PropertyName = "Free")]
        public bool Free { get; set; }

        [JsonProperty(PropertyName = "Price")]
        public string Price { get; set; }

        [JsonProperty(PropertyName = "SalePrice")]
        public string SalePrice { get; set; }

    }
}