using Api.Data;
using Api.Messaging;

namespace Api.Services {

    public interface ICoordinatesService {
        IQueryable<Messaging.Coordinate> GetCoordinates(Guid[] ids);
        IDictionary<string, Messaging.VehicleStats> CalculatePath(Messaging.Coordinate[] coordinates);
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

        public IDictionary<string, Messaging.VehicleStats> CalculatePath(Messaging.Coordinate[] coordinates) {
            if (coordinates == null || coordinates.Length == 0)
                return new Dictionary<string, Messaging.VehicleStats>();

            var result = coordinates
                .GroupBy(v => v.VehicleName)
                .ToDictionary(g => g.Key, CalculatePath);

            return result;
        }

        private double Distance(Messaging.Coordinate p1, Messaging.Coordinate p2, Utils.Math.DistanceMeasureUnit unit) {
            return Api.Utils.Math.Distance(p1.Longitude, p2.Longitude, p1.Latitude, p2.Latitude, unit);
        }

        private Messaging.VehicleStats CalculatePath(IGrouping<string, Messaging.Coordinate> group) {
            var result = new Messaging.VehicleStats(0, 0);

            var points = group.OrderByDescending(c => c.Timestamp).ToArray();

            if (points.Length > 1) {
                for(var i = 0; i < points.Length - 1; i++) {
                    var p1 = points[i];
                    var p2 = points[i + 1];

                    result.Meters += Distance(p1, p2, Utils.Math.DistanceMeasureUnit.METERS);
                    result.Miles += Distance(p1, p2, Utils.Math.DistanceMeasureUnit.MILES);
                }
            }

            return result;
        }
    }
}
