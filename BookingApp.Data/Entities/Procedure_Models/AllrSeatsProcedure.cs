using System;

namespace BookingApp.Data.Entities.Procedure_Models
{
    public partial class AllrSeatsProcedure
    {
        public Guid SeatId { get; set; }
        public int CarNumber { get; set; }
        public string CarType { get; set; }
        public int SeatNumber { get; set; }
        public double PriceCoeff { get; set; }
        public bool Free { get; set; }
        public virtual TypeCar TypeCar { get; set; }
    }
}
