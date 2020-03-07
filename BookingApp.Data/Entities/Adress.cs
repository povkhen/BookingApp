using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingApp.Data.Entities
{
    [Table("route.Adress")]
    public partial class Adress : BaseEntity
    {
        public Adress()
        {
            Stations = new HashSet<Station>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Town { get; set; }

        [Required]
        [StringLength(100)]
        public string Region { get; set; }

        [Required]
        [StringLength(100)]
        public string Street { get; set; }

        [Required]
        [StringLength(5)]
        public string Apartment { get; set; }

        [Required]
        [StringLength(100)]
        public string Country { get; set; }

        [StringLength(5)]
        public string PostalCode { get; set; }

        public virtual ICollection<Station> Stations { get; set; }
    }
}
