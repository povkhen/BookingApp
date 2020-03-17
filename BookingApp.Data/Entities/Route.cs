namespace BookingApp.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    [Table("route.Route")]
    public partial class Route : BaseEntity
    {
        public Route()
        {
            RailwayToRoutes = new HashSet<RailwayToRoute>();
            Trains = new HashSet<Train>();
        }


        public string Name { get; set; }

        public virtual ICollection<RailwayToRoute> RailwayToRoutes { get; set; }

        public virtual ICollection<Train> Trains { get; set; }
    }
}
