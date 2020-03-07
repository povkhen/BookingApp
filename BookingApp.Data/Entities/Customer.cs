namespace BookingApp.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("customer.Customer")]
    public partial class Customer : BaseEntity
    {
        public Customer()
        {
            Tickets = new HashSet<Ticket>();
        }

        public Guid? TypeCustomerId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public DateTime? BirthDay { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public virtual TypeCustomer TypeCustomer { get; set; }
    }
}
