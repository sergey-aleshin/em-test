namespace Api.Utils
{
    public static class Math
    {
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
    }
}
