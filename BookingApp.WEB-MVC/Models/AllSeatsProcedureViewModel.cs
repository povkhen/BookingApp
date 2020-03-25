using System;

namespace BookingApp.WEB_MVC.Models
{
    public partial class AllSeatsProcedureViewModel
    {
        public Guid SeatId { get; set; }
        public int CarNumber { get; set; }
        public string CarType { get; set; }
        public int SeatNumber { get; set; }
        public float PriceCoeff { get; set; }
        public bool Free { get; set; }
    }
}