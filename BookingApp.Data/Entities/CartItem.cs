using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingApp.Data.Entities
{
    [Table("customer.Cart")]
    public partial class CartItem : BaseEntity
    {
        public Guid CartId { get; set; }
        public Guid TicketId { get; set; }
        public int Quantity { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual Ticket Ticket { get; set; }
    }


}
