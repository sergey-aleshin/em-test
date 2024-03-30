using Api.Data;
using Api.Messaging;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Controlers
{
    [ApiController]
    [Route("[controller]")]
    public class CoordinatesController
    {
        private readonly ILogger<CoordinatesController> _logger;
        private readonly ApiDbContext _context;
        private readonly ICoordinatesService _coordinatesService;

        public CoordinatesController(ILogger<CoordinatesController> logger, ApiDbContext context, ICoordinatesService coordinatesService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _coordinatesService = coordinatesService ?? throw new ArgumentNullException(nameof(coordinatesService));
        }

        [HttpPost("find")]
        public async IAsyncEnumerable<Messaging.Coordinate> Find([FromBody] Guid[] ids) {
            var coordinates = _coordinatesService.GetCoordinates(ids).AsAsyncEnumerable();

            await foreach (var c in coordinates) { yield return c; }
        }

        [HttpPost("calculate-path")]
        public IDictionary<string, VehicleStats> CalculatePath([FromBody] Messaging.Coordinate[] coordinates) {
            if (coordinates == null || coordinates.Length == 0)
                return new Dictionary<string, VehicleStats>();

            var result = coordinates
                .GroupBy(v => v.VehicleName)
                .ToDictionary(g => g.Key, g => new VehicleStats(0, 0));

            return result;
        }
    }
}