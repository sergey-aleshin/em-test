using Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Api.Data
{
    public class ApiDbContext(DbContextOptions<ApiDbContext> options) : DbContext(options)
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Coordinate> Coordinates { get; set; }

        public void ResetDb()
        {
            Coordinates.RemoveRange(Coordinates);
            Vehicles.RemoveRange(Vehicles);
            SaveChanges();
        }

        public void PopulateDb(IConfiguration config)
        {
            var maxNumberOfVehicles = config.GetValue<int>("MaxNumberOfVehicles", 1);
            var maxNumberOfPointsPerVehicle = config.GetValue<int>("MaxNumberOfPointsPerVehicle", 1);

            foreach(var i in Enumerable.Range(1, Random.Shared.Next(1, maxNumberOfVehicles + 1))) {
                var vehicle = new Vehicle { Id = Guid.NewGuid(), Name = $"V{i}" };

                Vehicles.Add(vehicle);

                foreach (var j in Enumerable.Range(1, Random.Shared.Next(1, maxNumberOfPointsPerVehicle + 1)))
                {
                    var coordinate = new Coordinate
                    {
                        Latitude = Utils.Math.GetRandomLatitude(),
                        Longitude = Utils.Math.GetRandomLongitude(),
                        Timestamp = (j - 1) * 3,
                        VehicleId = vehicle.Id
                    };

                    Coordinates.Add(coordinate);
                }
            }

            SaveChanges();
        }
    }
}
