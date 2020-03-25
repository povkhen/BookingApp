using System;
using System.ComponentModel.DataAnnotations;

namespace BookingApp.WEB_MVC.Models.Binding
{
    public class MainSearchBind
    {
        [Required (ErrorMessage = "Будь-ласка, введіть станцію відправлення")]
        public string From { get; set; }

        [Required (ErrorMessage = "Будь-ласка, введіть станцію прибуття")]
        public string To { get; set; }

        [Required(ErrorMessage = "Будь-ласка, введіть дату відправлення")]
        public DateTime Date { get; set; }
        public string StartTime { get; set; } = "00";
    }
}