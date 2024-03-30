namespace Api.Messaging
{
    public class Coordinate(Guid id, double latitude, double longitude, long timestamp)
    {
        public Guid VehicleId { get; set; } = id;

        public double Latitude { get; set; } = latitude;
        
        public double Longitude { get; set; } = longitude;

        public long Timestamp { get; set; } = timestamp;
    }
}
