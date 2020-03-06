namespace BookingApp.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("schedule.Trip")]
    public partial class Trip : BaseEntity
    {
        public Trip()
        {
            Tickets = new HashSet<Ticket>();
        }

        public Guid TrainId { get; set; }

        public DateTime Datetime { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public virtual Train Train { get; set; }
    }
}
