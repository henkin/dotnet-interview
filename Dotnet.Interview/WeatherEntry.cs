using System;

namespace Dotnet.Interview
{
    public class WeatherEntry
    {
        public DateTime Date { get; set; }

        public decimal TemperatureC => (TemperatureF - 32) * (decimal) 0.5556; // 32 + (int)(TemperatureC / 0.5556);

        public int TemperatureF { get; set; }
        
        public string Summary { get; set; }
        
        public string City { get; set; }
    }
}