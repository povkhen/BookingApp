using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingApp.Data.Entities
{
    [Table("train.Car")]
    public partial class Car : BaseEntity
    {
        public Car()
        {
            Seats = new HashSet<Seat>();
        }

        public Guid TrainId { get; set; }

        public Guid? TypeCarId { get; set; }

        public short? Max_Capasity { get; set; }

        public double PriceCoefficient { get; set; }

        public short? Number { get; set; }

        public virtual Train Train { get; set; }

        public virtual TypeCar TypeCar { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }
    }
}
