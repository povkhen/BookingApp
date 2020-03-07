namespace BookingApp.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("schedule.RecurrenceDay")]
    public partial class RecurrenceDay : BaseEntity
    {
        public RecurrenceDay()
        {
            Times = new HashSet<Time>();
        }

        public Guid TrainId { get; set; }

        public int NumberDay { get; set; }

        public virtual Day Day { get; set; }

        public virtual Train Train { get; set; }

        public virtual ICollection<Time> Times { get; set; }
    }
}
