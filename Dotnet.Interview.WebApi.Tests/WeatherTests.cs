using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Dotnet.Interview.WebApi.WeatherForecast;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using FluentAssertions;

namespace Dotnet.Interview.WebApi.Tests
{
    public class WeatherTests
        : IClassFixture<WebApplicationFactory<Startup>>
    {
        private const string RequestUri = "/WeatherForecast";
        private readonly WebApplicationFactory<Startup> _factory;

        public WeatherTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData(RequestUri)]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task Get_Return200_And_WeatherForecasts()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(RequestUri);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var content = await response.Content.ReadAsStringAsync();
            var weatherEntries = DeserializeResponseString<IEnumerable<WeatherEntry>>(content);

            weatherEntries.Should().HaveCount(5);
            weatherEntries.Select(x => x.TemperatureF).Should().NotContain(temp => temp > 130 || temp < -40,
                "normal temperatures on earth");
        }

        [Fact]
        public async Task Post_ReportWeather_Return201AndWeatherEntryAndLocationHeader()
        {
            // Arrange
            var client = _factory.CreateClient();
            var expected = new WeatherEntryCreateRequest()
            {
                City = "Kirkland",
                Date = DateTime.Now,
                TemperatureF = 77
            };

            // Act
            var response = await client.PostAsync(RequestUri, GetJsonSerializedStringContent(expected));

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            // assert body of response deserializes into WeatherForecast

            //   and forecast has Id set;

            // assert response header Location points to created weather
            var actualId = 12345; // grab value from deserialized object
            response.Headers.Location.Should().Be($"/WeatherForecast/{actualId}"); 
        }

        private static StringContent GetJsonSerializedStringContent(object o)
        {
            var content = JsonSerializer.Serialize(o,
                new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            return stringContent;
        }
        
        private static TResponse DeserializeResponseString<TResponse>(string content) =>
            JsonSerializer
                .Deserialize<TResponse>(content, 
                    new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
    }
}