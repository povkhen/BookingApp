using BookingApp.Data.Entities;
using BookingApp.Data.Interfaces;
using BookingApp.Data.Repositories;


namespace BookingApp.Data.Infrastructure
{
    public class Context : IContext
    {

        private IStoredProcedures _procedures;
        private IStationRepository _stationRepo;
        private IRouteRepository _routeRepo;
        private readonly IRepository<Adress> _adressRepo;
        private readonly IRepository<Car> _carRepo;
        private readonly IRepository<Customer> _customerRepo;
        private readonly IRepository<Railway> _railwayRepo;
        private readonly IRepository<RailwayToRoute> _railwayToRouteRepo;
        private readonly IRepository<RecurrenceDay> _reccurenceDayRepo;
        private readonly IRepository<Seat> _seatRepo;
        private readonly IRepository<Ticket> _ticketRepo;
        private readonly IRepository<Time> _timeRepo;
        private readonly IRepository<Train> _trainRepo;
        private readonly IRepository<Trip> _tripRepo;
        private readonly IRepository<TypeCar> _typeCarRepo;
        private readonly IRepository<TypeCustomer> _typeCustomerRepo;
        private readonly IRepository<TypeSeat> _typeSeatRepo;
        private readonly IRepository<Cart> _cartRepo;
        private readonly IRepository<CartItem> _cartItemRepo;

        private IRepository<T> CreateRepo<T>(IRepository<T> _repo, string name) where T: BaseEntity
        {
            if (_repo == null)
            {
                _repo = new Repository<T>(name);
            }
            return _repo;
        }


        public IStoredProcedures StoredProcedures
        {
            get
            {
                if (_procedures == null)
                {
                    _procedures = new StoredProcedures();
                }
                return _procedures;
            }
        }
        public IStationRepository StationRepo 
        {
            get
            {
                if (_stationRepo == null)
                {
                    _stationRepo = new StationRepository("route.Station");
                }
                return _stationRepo;
            }
        }

        public IRouteRepository RouteRepo
        {
            get
            {
                if (_routeRepo == null)
                {
                    _routeRepo = new RouteRepository("route.Route");
                }
                return _routeRepo;
            }
        }
        public IRepository<Adress> AdressRepo
        {
            get => CreateRepo<Adress>(_adressRepo, "route.Adress");
        }

        public IRepository<Car> CarRepo
        {
            get => CreateRepo<Car>(_carRepo, "train.Car");
        }

        public IRepository<Customer> CustomerRepo
        {
            get => CreateRepo<Customer>(_customerRepo,"customer.Customer");
        }
        public IRepository<Railway> RailwayRepo
        {
            get => CreateRepo<Railway>(_railwayRepo, "route.Railway");
        }
        public IRepository<RailwayToRoute> RailwayToRouteRepo
        {
            get => CreateRepo<RailwayToRoute>(_railwayToRouteRepo, "route.RailwayToRoute");
        }
        public IRepository<RecurrenceDay> RecurrenceDayRepo
        {
            get => CreateRepo<RecurrenceDay>(_reccurenceDayRepo, "schedule.RecurrenceDay");
        }
        public IRepository<Seat> SeatRepo
        {
            get => CreateRepo<Seat>(_seatRepo, "train.Seat");
        }
        
        public IRepository<Ticket> TicketRepo
        {
            get => CreateRepo<Ticket>(_ticketRepo, "customer.Ticket");
        }
        public IRepository<Time> TimeRepo
        {
            get => CreateRepo<Time>(_timeRepo, "schedule.Time");
        }
        public IRepository<Train> TrainRepo
        {
            get => CreateRepo<Train>(_trainRepo, "train.Train");
        }
        public IRepository<Trip> TripRepo
        {
            get => CreateRepo<Trip>(_tripRepo, "schedule.Trip");
        }
        public IRepository<TypeCar> TypeCarRepo
        {
            get => CreateRepo<TypeCar>(_typeCarRepo, "train.TypeCar");
        }
        public IRepository<TypeCustomer> TypeCustomerRepo
        {
            get => CreateRepo<TypeCustomer>(_typeCustomerRepo, ("customer.TypeCustomer"));
        }
        public IRepository<TypeSeat> TypeSeatRepo
        {
            get => CreateRepo<TypeSeat>(_typeSeatRepo, "train.TypeSeat");
        }
        public IRepository<Cart> CartRepo
        {
            get => CreateRepo<Cart>(_cartRepo, "customer.Cart");
        }
        public IRepository<CartItem> CartItemRepo
        {
            get => CreateRepo<CartItem>(_cartItemRepo, "customer.CartItem");
        }
    }
}
