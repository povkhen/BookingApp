using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace BookingApp.WEB_MVC.Models.Binding
{
    public class MainSearchBind
    {
        [JsonProperty(PropertyName = "From")]
        [Required (ErrorMessage = "Будь-ласка, введіть станцію відправлення")]
        public string From { get; set; }


        [JsonProperty(PropertyName = "To")]
        [Required (ErrorMessage = "Будь-ласка, введіть станцію прибуття")]
        public string To { get; set; }

        [JsonProperty(PropertyName = "Date")]
        [Required(ErrorMessage = "Будь-ласка, введіть дату відправлення")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "StartTime")]
        public string StartTime { get; set; } = "00";
    }
}