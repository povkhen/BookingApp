using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookingApp.WEB_MVC.Models.Binding
{
    public class TicketBind
    {
        [JsonProperty(PropertyName = "Id")]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "FirstName")]
        [Required(ErrorMessage = "Будь-ласка, введіть ім'я")]
        public string FirstName { get; set; }


        [JsonProperty(PropertyName = "ToLastName")]
        [Required(ErrorMessage = "Будь-ласка, введіть прізвище")]
        public string LastName { get; set; }


        [JsonProperty(PropertyName = "TypeCustomer")]
        public string TypeCustomer { get; set; }
        public IEnumerable<string> Types { get; set; }

        [JsonProperty(PropertyName = "TripId")]
        public Guid TripId { get; set; }

        [JsonProperty(PropertyName = "SeatId")]
        public Guid SeatId { get; set; }

        [JsonProperty(PropertyName = "ArrivalStation")]
        public Guid? ArrivalStationId { get; set; }

        [JsonProperty(PropertyName = "DepartureStation")]
        public Guid? DepartureStationId { get; set; }

        [JsonProperty(PropertyName = "DepartureTime")]
        public DateTime DepartureTime { get; set; }

        [JsonProperty(PropertyName = "ArrivalTime")]
        public DateTime ArrivalTime { get; set; }

        [JsonProperty(PropertyName = "Price")]
        public string Price { get; set; }
    }
}