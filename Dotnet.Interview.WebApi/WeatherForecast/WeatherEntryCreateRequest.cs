using System;
using System.ComponentModel.DataAnnotations;

namespace Dotnet.Interview.WebApi.WeatherForecast
{
    public class WeatherEntryCreateRequest
    {
        [Required]
        [MinLength(2)]
        public string City { get; set; }

        public DateTime Date { get; set; }

        public int TemperatureF { get; set; }
    }
}
