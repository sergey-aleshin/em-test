using Api.Data;
using Api.Models;
using Api.Services;
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
        private readonly IVehiclesService _vehiclesService;

        public VehiclesController(ILogger<VehiclesController> logger, ApiDbContext context, IVehiclesService vehiclesService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _vehiclesService = vehiclesService ?? throw new ArgumentNullException(nameof(vehiclesService));
        }

        [HttpGet]
        public async IAsyncEnumerable<Messaging.Vehicle> Get()
        {
            var vehicles = _vehiclesService.GetVehicles().AsAsyncEnumerable();

            await foreach (var v in vehicles) { yield return v; }
        }
    }
}
