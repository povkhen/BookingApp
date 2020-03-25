using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingApp.Data.Entities
{
    [Table("customer.Cart")]
    public partial class Cart : BaseEntity
    {
        [StringLength(255)]
        public string SessionId { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }


}
