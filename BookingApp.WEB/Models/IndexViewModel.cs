using System.Collections.Generic;

namespace BookingApp.WEB.Models
{
    public class IndexViewModel<T> where T: class
    {
        public IEnumerable<T> Models { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}