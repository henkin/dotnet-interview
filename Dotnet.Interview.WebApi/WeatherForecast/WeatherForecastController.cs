using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Dotnet.Interview.WebApi.WeatherForecast
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = 
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        
        private static readonly string[] Cities = 
        {
            "Seattle", "Bellevue", "New York"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<RetrieveViewModel> Get()
        {
            var rng = new Random();
            var weatherEntries = Enumerable.Range(1, 5).Select(index =>
                {
                    var x = rng.Next(Summaries.Length);

                    var temperatureF = 10 * x + 20 + rng.Next(10);
                    return new RetrieveViewModel
                    {
                        Date = DateTime.Now.AddDays(index),
                        TemperatureF = temperatureF, // Freezing == 20, Warm = 70
                        City = Cities[rng.Next(Cities.Length)],
                        Summary = Summaries[x]
                    };
                }).ToArray();

            return weatherEntries;
        }

        [HttpPost]
        public RetrieveViewModel Post(CreateViewModel createViewModel)
        {
            var retrieveViewModel = new RetrieveViewModel();
            
            return retrieveViewModel;
        }
    }
}
