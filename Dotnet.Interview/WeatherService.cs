using System;
using System.Collections.Generic;
using System.Linq;

namespace Dotnet.Interview
{
    public class WeatherService
    {
        private static readonly string[] Summaries = 
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        
        private static readonly string[] Cities = 
        {
            "Seattle", "Bellevue", "New York"
        };

        public IEnumerable<WeatherEntry> WeatherEntries { get; private set; }

        public WeatherService()
        {
            WeatherEntries = GetWeatherEntries();
        }

        public IEnumerable<WeatherEntry> GetWeatherEntries()
        {
            var rng = new Random();
            var weatherEntries = Enumerable.Range(1, 5).Select(index =>
            {
                var x = rng.Next(Summaries.Length);

                var temperatureF = 10 * x + 20 + rng.Next(10);
                return new WeatherEntry
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureF = temperatureF, // Freezing == 20, Warm = 70
                    City = Cities[rng.Next(Cities.Length)],
                    Summary = Summaries[x]
                };
            }).ToArray();

            return weatherEntries;
        }
    }
}