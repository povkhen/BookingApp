namespace BookingApp.Data.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("customer.TypeCustomer")]
    public partial class TypeCustomer : BaseEntity
    {
        public TypeCustomer()
        {
            Customers = new HashSet<Customer>();
        }

        public string Name { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
