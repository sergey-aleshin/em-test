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

        public void PopulateDb()
        {
            foreach(var i in Enumerable.Range(1, Random.Shared.Next(1, 100))) {
                var vehicle = new Vehicle { Id = Guid.NewGuid(), Name = $"V{i}" };

                Vehicles.Add(vehicle);

                foreach (var j in Enumerable.Range(1, Random.Shared.Next(1, 1000)))
                {
                    var coordinate = new Coordinate
                    {
                        Latitude = 0,
                        Longitude = 0,
                        Timestamp = j * 3,
                        VehicleId = vehicle.Id
                    };

                    Coordinates.Add(coordinate);
                }
            }

            SaveChanges();
        }
    }
}
