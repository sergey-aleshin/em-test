using Api.Data;

namespace Api.Services {

    public interface ICoordinatesService {
        IQueryable<Messaging.Coordinate> GetCoordinates(Guid[] ids);
    }

    public class CoordinatesService: ICoordinatesService
    {
        private readonly ApiDbContext _context;

        public CoordinatesService(ApiDbContext context) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<Messaging.Coordinate> GetCoordinates(Guid[] ids) {
            var coordinates = _context.Coordinates
                .Where(c => ids.Contains(c.VehicleId))
                .OrderBy(c => c.Vehicle.Name).ThenByDescending(c => c.Timestamp)
                .Select(c => new Messaging.Coordinate(c.VehicleId, c.Vehicle.Name, c.Latitude, c.Longitude, c.Timestamp));
            
            return coordinates;
        }
    }
}
