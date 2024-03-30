namespace Api.Messaging
{
    public class Coordinate(Guid id, string name, double latitude, double longitude, long timestamp)
    {
        public Guid Id { get; set; } = id;

        public string Name { get; set; } = name;

        public double Latitude { get; set; } = latitude;
        
        public double Longitude { get; set; } = longitude;

        public long Timestamp { get; set; } = timestamp;
    }
}
