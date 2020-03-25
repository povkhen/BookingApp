using System;
using System.Collections.Generic;
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

        public string Town { get; set; }
        public string Region { get; set; }
        public string Street { get; set; }
        public string Apartment { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        public virtual ICollection<Station> Stations { get; set; }
    }

}
