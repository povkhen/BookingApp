﻿using System.Collections.Generic;

namespace BookingApp.WEB_MVC.Models
{
    public class TripViewModel<T> where T: class
    {
        public IEnumerable<T> Models { get; set; }
        public string Type { get; set; }
    }
}