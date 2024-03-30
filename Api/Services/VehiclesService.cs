using Api.Data;

namespace Api.Services {

    public interface IVehiclesService
    {
        IQueryable<Messaging.Vehicle> GetVehicles();
    }

    public class VehiclesService: IVehiclesService
    {
        private readonly ApiDbContext _context;

        public VehiclesService(ApiDbContext context) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public IQueryable<Messaging.Vehicle> GetVehicles() {
            var vehicles = _context.Vehicles
                .OrderBy(v => v.Name)
                .Select(v => new Messaging.Vehicle(v.Id, v.Name));
            
            return vehicles;
        }
    }
}
