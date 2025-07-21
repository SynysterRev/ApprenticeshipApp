using JuniorOnly.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Mvc;

namespace JuniorOnly.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var canConnect = await _context.Database.CanConnectAsync();
            return Ok(new { DatabaseConnected = canConnect });
        }

        [HttpGet("{sessionID}")]
        public async Task<IActionResult> Get(int sessionID)
        {
            return Ok("Salut mon bro");
        }
    }
}
