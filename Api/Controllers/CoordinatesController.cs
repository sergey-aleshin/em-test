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
        private readonly ICoordinatesService _coordinatesService;

        public CoordinatesController(ICoordinatesService coordinatesService)
        {
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

            return _coordinatesService.CalculatePath(coordinates);
        }
    }
}