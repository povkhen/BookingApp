namespace BookingApp.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("train.TypeSeat")]
    public partial class TypeSeat : BaseEntity
    {
        public TypeSeat()
        {
            Seats = new HashSet<Seat>();
        }

        [Required]
        [StringLength(15)]
        public string Name { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }
    }
}
