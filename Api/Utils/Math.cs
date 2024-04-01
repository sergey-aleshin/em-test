using Microsoft.Extensions.Diagnostics.Metrics;
using System.Net.NetworkInformation;

namespace Api.Utils
{
    public static class Math
    {
        public const int EarthRadiusInMeters = 6371000;
        public const int EarthRadiusInMiles = 3956;

        public enum DistanceMeasureUnit
        {
            METERS = 1,
            MILES = 2
        }

        public static double GetRandomDoubleInRange(int min, int max)
        {
            var randomValue = Random.Shared.Next(min, max + 1);
            var randomDouble = randomValue * Random.Shared.NextDouble();

            return randomDouble;
        }

        public static double GetRandomLatitude()
        {
            return GetRandomDoubleInRange(-90, 90);
        }

        public static double GetRandomLongitude()
        {
            return GetRandomDoubleInRange(-180, 180);
        }

        public static double ToRadians(this double angle)
        {
            return (angle * System.Math.PI) / 180;
        }

        public static double Distance(double lon1, double lon2, double lat1, double lat2, DistanceMeasureUnit unit)
        {
            
            var phi1 = lat1.ToRadians();
            var phi2 = lat2.ToRadians();
            var delta_phi = (lat2 - lat1).ToRadians();
            var delta_lambda = (lon2 - lon1).ToRadians();

            var a = System.Math.Sin(delta_phi / 2) * System.Math.Sin(delta_phi / 2) +
                    System.Math.Cos(phi1) * System.Math.Cos(phi2) *
                    System.Math.Sin(delta_lambda / 2) * System.Math.Sin(delta_lambda / 2);
            var c = 2 * System.Math.Atan2(System.Math.Sqrt(a), System.Math.Sqrt(1 - a));

            double radius = unit == DistanceMeasureUnit.METERS ? EarthRadiusInMeters : EarthRadiusInMiles;
            
            var d = radius * c;

            return d;
        }        
    }
}
