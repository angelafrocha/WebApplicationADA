using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApplicationADA.Options;

namespace WebApplicationADA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IOptionsMonitor<WeatherOptions> options;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IOptionsMonitor<WeatherOptions> options)
        {
            _logger = logger;
            this.options = options;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            if (options.CurrentValue.UseFixedValue)
            {
                return new List<WeatherForecast>() { new WeatherForecast{
                    Date = DateOnly.Parse(options.CurrentValue.FixedDate),
                    TemperatureC = 25,
                    Summary = Summaries[5]
                    
                } };
            }

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}