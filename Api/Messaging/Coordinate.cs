namespace Api.Messaging
{
    public class Coordinate(Guid vehicleId, string vehicleName, double latitude, double longitude, long timestamp)
    {
        public Guid VehicleId { get; set; } = vehicleId;

        public string VehicleName { get; set; } = vehicleName;

        public double Latitude { get; set; } = latitude;
        
        public double Longitude { get; set; } = longitude;

        public long Timestamp { get; set; } = timestamp;
    }
}
