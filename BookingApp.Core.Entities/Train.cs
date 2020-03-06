namespace BookingApp.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("train.Train")]
    public partial class Train : BaseEntity
    {
        public Train()
        {
            RecurrenceDays = new HashSet<RecurrenceDay>();
            Trips = new HashSet<Trip>();
            Cars = new HashSet<Car>();
        }

        public Guid RouteId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public double AvarageSpeed { get; set; }

        public virtual Route Route { get; set; }

        public virtual ICollection<RecurrenceDay> RecurrenceDays { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
