using System;

namespace BookingApp.Core.DTO
{
    public partial class AllrSeatsProcedureDTO
    {
        public Guid SeatId { get; set; }
        public int CarNumber { get; set; }
        public string CarType { get; set; }
        public int SeatNumber { get; set; }
        public double PriceCoeff { get; set; }
        public bool Free { get; set; }
    }
}
