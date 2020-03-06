namespace BookingApp.Core.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("customer.Ticket")]
    public partial class Ticket : BaseEntity
    {
        
        public Guid CustomerId { get; set; }

        public Guid SeatId { get; set; }

        public Guid TripId { get; set; }

        public Guid ArrivalStationId { get; set; }

        public Guid DepartureStationId { get; set; }

        public DateTime ArrivalTime { get; set; }

        public DateTime? DepartureTime { get; set; }

        public double? Price { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Station Station { get; set; }

        public virtual Station Station1 { get; set; }

        public virtual Seat Seat { get; set; }

        public virtual Trip Trip { get; set; }
    }
}
