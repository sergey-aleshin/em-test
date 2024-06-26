﻿using Api.Utils;

namespace Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class MathTests
    {
        [Test]
        public void ToRadiansTest()
        { 
            var delta = 0.00001;
            var values = new[] { 0.0, 90.0, 180.0 };
            var expectedValues = new[] { 0.0, System.Math.PI / 2, System.Math.PI };
            
            for(var i = 0; i < values.Length; i++)
            {
                Assert.That(values[i].ToRadians(), Is.EqualTo(expectedValues[i]).Within(delta));
            }
        }

        [Test]
        public void DistanceTest1()
        {
            var moscow_lat = 55.755773;
            var moscow_lon = 37.617761;
            var izhevsk_lat = 56.852775;
            var izhevsk_lon = 53.211463;
            var expected_distance = 967.5 * 1000;
            var delta = 50;
            var distance = Api.Utils.Math.Distance(moscow_lon, izhevsk_lon, moscow_lat, izhevsk_lat, Api.Utils.Math.DistanceMeasureUnit.METERS);

            Assert.That(distance, Is.EqualTo(expected_distance).Within(delta));
        }

        [Test]
        public void DistanceTest2()
        {
            var moscow_lat = 55.755773;
            var moscow_lon = 37.617761;
            var spb_lat = 59.938806;
            var spb_lon = 30.314278;
            var expected_distance = 634.2 * 1000;
            var delta = 50;
            var distance = Api.Utils.Math.Distance(moscow_lon, spb_lon, moscow_lat, spb_lat, Api.Utils.Math.DistanceMeasureUnit.METERS);

            Assert.That(distance, Is.EqualTo(expected_distance).Within(delta));
        }
    }
}
