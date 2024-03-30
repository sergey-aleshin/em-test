using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controlers
{
    [ApiController]
    [Route("[controller]")]
    public class CoordinatesController
    {
        private readonly ILogger<CoordinatesController> _logger;
        private readonly ApiDbContext _context;

        public CoordinatesController(ILogger<CoordinatesController> logger, ApiDbContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpPost("find")]
        public async IAsyncEnumerable<Messaging.Coordinate> Find([FromBody] Guid[] ids) {
            var coordinates = _context.Coordinates
                .Where(c => ids.Contains(c.VehicleId))
                .OrderBy(c => c.Vehicle.Name).ThenByDescending(c => c.Timestamp)
                .Select(c => new Messaging.Coordinate(c.VehicleId, c.Latitude, c.Longitude, c.Timestamp))
                .AsAsyncEnumerable();

            await foreach (var c in coordinates) { yield return c; }
        }

        [HttpPost("calculate-path")]
        public IActionResult CalculatePath() {
            return new NotFoundResult();
        }
    }
}