using BookingApp.Core.BusinessModels;
using System.Collections.Generic;

namespace BookingApp.WEB_MVC.Models
{
    public class IndexViewModel<T> where T: class
    {
        public IEnumerable<T> Models { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}