namespace BookingApp.Core.Entities
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

        [Required]
        [StringLength(15)]
        public string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
