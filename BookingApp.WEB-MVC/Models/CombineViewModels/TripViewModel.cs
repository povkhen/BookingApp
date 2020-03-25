using BookingApp.WEB_MVC.Models.Binding;
using System.Collections.Generic;

namespace BookingApp.WEB_MVC.Models
{
    public class TripViewModel<T> where T: class
    {
        public IEnumerable<T> Models { get; set; }
        public MainSearchBind Bind { get; set; }

    }
}