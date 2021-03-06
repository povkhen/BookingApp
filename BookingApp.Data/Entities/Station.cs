using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingApp.Data.Entities
{
    [Table("route.Station")]
    public partial class Station : BaseEntity
    {
        public Station()
        {
            Tickets = new HashSet<Ticket>();
            Tickets1 = new HashSet<Ticket>();
            Railways = new HashSet<Railway>();
            Railways1 = new HashSet<Railway>();
        }

        public Guid AdressId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public virtual ICollection<Ticket> Tickets1 { get; set; }

        public virtual Adress Adress { get; set; }

        public virtual ICollection<Railway> Railways { get; set; }

        public virtual ICollection<Railway> Railways1 { get; set; }
    }
}
