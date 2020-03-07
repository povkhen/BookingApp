namespace BookingApp.Data.Entities.Procedure_Models
{
    public partial class TypeCarSeats
    {   
        public string Car { get; set; }
        public string Count { get; set; }
        public virtual TypeCar TypeCar { get; set; }
    }
}
