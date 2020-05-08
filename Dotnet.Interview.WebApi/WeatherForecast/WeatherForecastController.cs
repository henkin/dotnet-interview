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

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherService _weatherService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _weatherService = new WeatherService();
        }

        [HttpGet]
        public IEnumerable<WeatherEntry> Get()
        {
            var entries = _weatherService.GetWeatherEntries();
            return entries;
        }

        [HttpPost]
        public WeatherEntry Post([FromBody] WeatherEntryCreateRequest createViewModel)
        {
            var retrieveViewModel = new WeatherEntry();
            
            return retrieveViewModel;
            
            // try to add entry
            
            // return error httpCode if fails
            
            // return 201 and the created WeatherEntry if success 
        }
    }
    
}