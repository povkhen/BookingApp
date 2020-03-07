namespace BookingApp.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("train.Seat")]
    public partial class Seat : BaseEntity
    {
        public Seat()
        {
            Tickets = new HashSet<Ticket>();
        }

        public Guid CarId { get; set; }

        public Guid? TypeSeatId { get; set; }

        public short Number { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public virtual Car Car { get; set; }

        public virtual TypeSeat TypeSeat { get; set; }
    }
}
