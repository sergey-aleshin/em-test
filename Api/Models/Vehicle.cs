using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Vehicle
    {
        public Guid Id { get; set; }

        [MaxLength(30)]
        public required string Name { get; set;}

        public IList<Coordinate> Coordinates { get; set; } = new List<Coordinate>();
    }
}
