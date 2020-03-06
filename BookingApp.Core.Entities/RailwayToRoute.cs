namespace BookingApp.Core.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("route.RailwayToRoute")]
    public partial class RailwayToRoute : BaseEntity
    {
        public Guid RailwayId { get; set; }

        public Guid RouteId { get; set; }

        public short SequenceNumber { get; set; }

        public double StopTime { get; set; }

        public virtual Railway Railway { get; set; }

        public virtual Route Route { get; set; }
    }
}
