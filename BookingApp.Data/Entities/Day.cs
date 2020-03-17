using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingApp.Data.Entities
{
    [Table("schedule.Day")]
    public partial class Day
    {
        public Day()
        {
            RecurrenceDays = new HashSet<RecurrenceDay>();
        }

        public int Number { get; set; }

        public string DayName { get; set; }

        public virtual ICollection<RecurrenceDay> RecurrenceDays { get; set; }
    }
}
