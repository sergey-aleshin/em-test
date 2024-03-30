namespace Api.Messaging {
    public class VehicleStats(double distanceInMeters, double distanceInMiles)
    {
            public double Metres { get; set; } = distanceInMeters;

            public double Miles { get; set; } = distanceInMiles;
    }
}