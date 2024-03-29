namespace Api.Models
{
    public class Coordinate
    {
        public int Id { get; set; }

        public Guid VehicleId { get; set; }

        public Vehicle Vehicle { get; set; } = null!;

        public required double Latitude { get; set; }
        
        public required double Longitude { get; set; }

        public required long Timestamp { get; set; }
    }
}
