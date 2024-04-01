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
        private readonly IVehiclesService _vehiclesService;

        public VehiclesController(IVehiclesService vehiclesService)
        {
            _vehiclesService = vehiclesService;
        }

        [HttpGet]
        public async IAsyncEnumerable<Messaging.Vehicle> Get()
        {
            var vehicles = _vehiclesService.GetVehicles().AsAsyncEnumerable();

            await foreach (var v in vehicles) { yield return v; }
        }
    }
}
