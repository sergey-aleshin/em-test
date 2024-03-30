using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehiclesController
    {
        private readonly ILogger<VehiclesController> _logger;
        private readonly ApiDbContext _context;

        public VehiclesController(ILogger<VehiclesController> logger, ApiDbContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public async IAsyncEnumerable<Messaging.Vehicle> Get()
        {
            var vehicles = _context.Vehicles
                .OrderBy(v => v.Name)
                .Select(v => new Messaging.Vehicle(v.Id, v.Name)).AsAsyncEnumerable();

            await foreach (var v in vehicles) { yield return v; }
        }
    }
}
