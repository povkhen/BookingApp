namespace BookingApp.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("customer.TypeCustomer")]
    public partial class TypeCustomer : BaseEntity
    {
        public TypeCustomer()
        {
            Customers = new HashSet<Customer>();
        }

        [Required]
        [StringLength(15)]
        public string Name { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
