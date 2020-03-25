using BookingApp.Data.Entities;

namespace BookingApp.Data.Interfaces
{
    public interface IContext
    {
        IStoredProcedures StoredProcedures { get; }
        IStationRepository StationRepo { get; }
        IRepository<Adress> AdressRepo { get; }
        IRepository<Car> CarRepo { get; }
        IRepository<Customer> CustomerRepo { get; }
        IRepository<Railway> RailwayRepo { get; }
        IRepository<RailwayToRoute> RailwayToRouteRepo { get; }
        IRepository<RecurrenceDay> RecurrenceDayRepo { get; }
        IRouteRepository RouteRepo { get; }
        IRepository<Seat> SeatRepo { get; }
        IRepository<Ticket> TicketRepo { get; }
        IRepository<Time> TimeRepo { get; }
        IRepository<Train> TrainRepo { get; }
        IRepository<Trip> TripRepo { get; }
        IRepository<TypeCar> TypeCarRepo { get; }
        IRepository<TypeCustomer> TypeCustomerRepo { get; }
        IRepository<TypeSeat> TypeSeatRepo { get; }
        IRepository<Cart> CartRepo { get; }
        IRepository<CartItem> CartItemRepo { get; }

    }
}
