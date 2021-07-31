using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static System.DateTime;

namespace CatalogBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
            {"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"};

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("Going to generate the weather forecast");
            var random = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = Now.AddDays(index),
                    TemperatureC = random.Next(-20, 55),
                    Summary = Summaries[random.Next(Summaries.Length)]
                })
                .ToArray();
        }
    }
}
