using System.Collections.Generic;
using System.Net.Http;
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
        public async Task Post_ReportWeather_Return201_Body_And_Location()
        {
            // Arrange
            var client = _factory.CreateClient();
            var expected = new CreateViewModel()
            {
                City = "Kirkland",
                TemperatureF = 77
            };
            
            // Act
            var response = await client.PostAsync(RequestUri,
                new StringContent(JsonSerializer.Serialize(expected))
            );

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            
            // assert body of response deserializes into WeatherForecast

            //   and forecast has Id set;

            // assert response header Location points to created weather
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
            var viewModels = JsonSerializer
                .Deserialize<IEnumerable<RetrieveViewModel>>(content);
            
            viewModels.Should().HaveCountLessThan(5);
        }
    }
}