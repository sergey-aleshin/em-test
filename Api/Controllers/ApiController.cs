using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("/")]
    public class ApiController
    {
        private readonly ILogger<ApiController> _logger;
        private readonly ApiDbContext _context;

        public ApiController(ILogger<ApiController> logger, ApiDbContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet("vehicles")]
        public async IAsyncEnumerable<Messaging.Vehicle> GetVehicles()
        {
            var vehicles = _context.Vehicles
                .OrderBy(v => v.Name)
                .Select(v => new Messaging.Vehicle(v.Id, v.Name)).AsAsyncEnumerable();

            await foreach(var v in vehicles) { yield return v; }
        }
    }
}
