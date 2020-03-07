namespace BookingApp.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("route.Railway")]
    public partial class Railway : BaseEntity
    {
        public Railway()
        {
            RailwayToRoutes = new HashSet<RailwayToRoute>();
        }

        
        public Guid Station1Id { get; set; }

        public Guid Station2Id { get; set; }

        public decimal Distance { get; set; }

        public virtual Station Station { get; set; }

        public virtual Station Station1 { get; set; }

        public virtual ICollection<RailwayToRoute> RailwayToRoutes { get; set; }
    }
}
