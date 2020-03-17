namespace BookingApp.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("train.TypeCar")]
    public partial class TypeCar : BaseEntity
    {
        public TypeCar()
        {
            Cars = new HashSet<Car>();
        }

        public string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
