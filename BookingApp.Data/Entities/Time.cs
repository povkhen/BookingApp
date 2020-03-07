namespace BookingApp.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("schedule.Time")]
    public partial class Time : BaseEntity
    {
        public Guid RecurrenceDayId { get; set; }

        [Column("Time")]
        public TimeSpan Time1 { get; set; }

        public virtual RecurrenceDay RecurrenceDay { get; set; }
    }
}
