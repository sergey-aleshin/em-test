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
    }
}