namespace BookingApp.Data.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("train.TypeSeat")]
    public partial class TypeSeat : BaseEntity
    {
        public TypeSeat()
        {
            Seats = new HashSet<Seat>();
        }

        public string Name { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }
    }
}
