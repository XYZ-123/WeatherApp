using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPITests
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;

    using Moq.Protected;
    using Moq;

    using WeatherAPI.Models;
    using WeatherAPI.Services.OpenWeatherMapService;

    using Xunit;

    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class OpenWeatherMapServiceTests
    {
        private Mock<HttpMessageHandler> mockedMessageHandler;

        private OpenWeatherMapService sut;

        private ForecastRequest request;
        public OpenWeatherMapServiceTests()
        {
            this.mockedMessageHandler = new Mock<HttpMessageHandler>();
            this.sut = new OpenWeatherMapService(new HttpClient(this.mockedMessageHandler.Object));
            this.request = new ForecastRequest
                               {
                                   Location = new GeoLocation { City = "London", Country = "uk" },
                                   Units = MeasureUnits.Celsium
                               };
        }

        [Fact]
        public async void GetWeather_ShouldReturn_CurrentWeather()
        {
            var message =
                "{ \"coord\":{ \"lon\":139,\"lat\":35},\"sys\":{ \"country\":\"JP\",\"sunrise\":1369769524,\"sunset\":1369821049},"
                + "\"weather\":[{\"id\":804,\"main\":\"clouds\",\"description\":\"overcast clouds\",\"icon\":\"04n\"}],"
                + "\"main\":{\"temp\":289.5,\"humidity\":89,\"pressure\":1013,\"temp_min\":287.04,\"temp_max\":292.04},"
                + "\"wind\":{\"speed\":7.31,\"deg\":187},"
                + "\"rain\":{\"3h\":0},"
                + "\"clouds\":{\"all\":92}," 
                + "\"dt\":1369824698,\"id\":1851632,\"name\":\"Shuzenji\",\"cod\":200}";
            var expected = new CurrentWeather
                               {
                                   CurrentTemprature = 289.5,
                                   Date = new DateTime(635894497369824698),
                                   Humidity = 89,
                                   Location = new GeoLocation { City = "Shuzenji"},
                                   Precipation = 0,
                                   Pressure = 1013,
                                   RainingProbability = 0,
                                   Units = MeasureUnits.Celsium,
                                   Wind = new Wind { Degree = 187, Speed = 7.31},
                                   WeatherDescription = "clouds\r\novercast clouds"
            };
            this.mockedMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),ItExpr.IsAny<CancellationToken>()).ReturnsAsync(new HttpResponseMessage {Content = new StringContent(message),StatusCode = HttpStatusCode.OK});

            var actual = await this.sut.GetCurrentWeatherAsync(this.request);
            Assert.True(AssertExtenstions.DeepEqual(expected,actual));
        }
    }
}
