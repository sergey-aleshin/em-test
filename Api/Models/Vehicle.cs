using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
    public class Vehicle
    {
        public Guid Id { get; set; }

        public required string Name { get; set;}

        public IList<Coordinate> Coordinates { get; set; } = new List<Coordinate>();
    }
}
