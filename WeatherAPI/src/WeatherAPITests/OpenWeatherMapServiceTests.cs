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

    using Newtonsoft.Json;

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
                                   Date = DateTime.Today + TimeSpan.FromTicks(1369824698),
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

        [Fact]
        public async void GetDailyForecast_ShouldReturnForecast()
        {
            var message = ForecastMessage.Message;
            var fromDate = new DateTime(2016, 1, 29);
            var toDate = new DateTime(2016, 1, 30);
            var expected =
                "[{\"location\":null,\"MinTemprature\":269.36,\"MaxTemprature\":279.37,\"Units\":0,\"Pressure\":988.6712500000001,\"Humidity\":84.75,\"RainingProbability\":0,\"Date\":\"2016-01-29T00:00:00\",\"Hourly\":[{\"Temperature\":279.0,\"Units\":0,\"Pressure\":984.25,\"Wind\":{\"speed\":5.75,\"deg\":257.0},\"Humidity\":77.0,\"RainingProbability\":0,\"Date\":\"2016-01-29T00:00:00\",\"WeatherCode\":500,\"WeatherDescription\":\"Rain\\r\\nlight rain\"},{\"Temperature\":278.0,\"Units\":0,\"Pressure\":984.75,\"Wind\":{\"speed\":5.36,\"deg\":291.502},\"Humidity\":90.0,\"RainingProbability\":0,\"Date\":\"2016-01-29T03:00:00\",\"WeatherCode\":500,\"WeatherDescription\":\"Rain\\r\\nlight rain\"},{\"Temperature\":275.0,\"Units\":0,\"Pressure\":985.32,\"Wind\":{\"speed\":5.81,\"deg\":306.523},\"Humidity\":90.0,\"RainingProbability\":0,\"Date\":\"2016-01-29T06:00:00\",\"WeatherCode\":800,\"WeatherDescription\":\"Clear\\r\\nsky is clear\"},{\"Temperature\":272.0,\"Units\":0,\"Pressure\":986.7,\"Wind\":{\"speed\":6.01,\"deg\":302.004},\"Humidity\":85.0,\"RainingProbability\":0,\"Date\":\"2016-01-29T09:00:00\",\"WeatherCode\":800,\"WeatherDescription\":\"Clear\\r\\nsky is clear\"},{\"Temperature\":272.0,\"Units\":0,\"Pressure\":989.09,\"Wind\":{\"speed\":5.01,\"deg\":301.001},\"Humidity\":88.0,\"RainingProbability\":0,\"Date\":\"2016-01-29T12:00:00\",\"WeatherCode\":600,\"WeatherDescription\":\"Snow\\r\\nlight snow\"},{\"Temperature\":273.0,\"Units\":0,\"Pressure\":992.19,\"Wind\":{\"speed\":4.46,\"deg\":317.011},\"Humidity\":86.0,\"RainingProbability\":0,\"Date\":\"2016-01-29T15:00:00\",\"WeatherCode\":600,\"WeatherDescription\":\"Snow\\r\\nlight snow\"},{\"Temperature\":275.0,\"Units\":0,\"Pressure\":993.55,\"Wind\":{\"speed\":5.46,\"deg\":303.004},\"Humidity\":84.0,\"RainingProbability\":0,\"Date\":\"2016-01-29T18:00:00\",\"WeatherCode\":800,\"WeatherDescription\":\"Clear\\r\\nsky is clear\"},{\"Temperature\":275.0,\"Units\":0,\"Pressure\":993.52,\"Wind\":{\"speed\":4.22,\"deg\":291.001},\"Humidity\":78.0,\"RainingProbability\":0,\"Date\":\"2016-01-29T21:00:00\",\"WeatherCode\":800,\"WeatherDescription\":\"Clear\\r\\nsky is clear\"}],\"Code\":800,\"Precipation\":0},{\"location\":null,\"MinTemprature\":266.022,\"MaxTemprature\":282.994,\"Units\":0,\"Pressure\":992.71375000000012,\"Humidity\":74.125,\"RainingProbability\":0,\"Date\":\"2016-01-30T00:00:00\",\"Hourly\":[{\"Temperature\":271.0,\"Units\":0,\"Pressure\":994.7,\"Wind\":{\"speed\":2.28,\"deg\":262.0},\"Humidity\":76.0,\"RainingProbability\":0,\"Date\":\"2016-01-30T00:00:00\",\"WeatherCode\":800,\"WeatherDescription\":\"Clear\\r\\nsky is clear\"},{\"Temperature\":267.0,\"Units\":0,\"Pressure\":995.25,\"Wind\":{\"speed\":1.31,\"deg\":213.001},\"Humidity\":76.0,\"RainingProbability\":0,\"Date\":\"2016-01-30T03:00:00\",\"WeatherCode\":800,\"WeatherDescription\":\"Clear\\r\\nsky is clear\"},{\"Temperature\":267.0,\"Units\":0,\"Pressure\":994.32,\"Wind\":{\"speed\":3.42,\"deg\":169.508},\"Humidity\":69.0,\"RainingProbability\":0,\"Date\":\"2016-01-30T06:00:00\",\"WeatherCode\":801,\"WeatherDescription\":\"Clouds\\r\\nfew clouds\"},{\"Temperature\":269.0,\"Units\":0,\"Pressure\":992.78,\"Wind\":{\"speed\":4.46,\"deg\":178.001},\"Humidity\":65.0,\"RainingProbability\":0,\"Date\":\"2016-01-30T09:00:00\",\"WeatherCode\":800,\"WeatherDescription\":\"Clear\\r\\nsky is clear\"},{\"Temperature\":270.0,\"Units\":0,\"Pressure\":991.67,\"Wind\":{\"speed\":5.32,\"deg\":184.501},\"Humidity\":69.0,\"RainingProbability\":0,\"Date\":\"2016-01-30T12:00:00\",\"WeatherCode\":803,\"WeatherDescription\":\"Clouds\\r\\nbroken clouds\"},{\"Temperature\":274.0,\"Units\":0,\"Pressure\":990.55,\"Wind\":{\"speed\":6.15,\"deg\":190.506},\"Humidity\":72.0,\"RainingProbability\":0,\"Date\":\"2016-01-30T15:00:00\",\"WeatherCode\":802,\"WeatherDescription\":\"Clouds\\r\\nscattered clouds\"},{\"Temperature\":282.0,\"Units\":0,\"Pressure\":991.19,\"Wind\":{\"speed\":6.71,\"deg\":234.001},\"Humidity\":85.0,\"RainingProbability\":0,\"Date\":\"2016-01-30T18:00:00\",\"WeatherCode\":601,\"WeatherDescription\":\"Snow\\r\\nsnow\"},{\"Temperature\":283.0,\"Units\":0,\"Pressure\":991.25,\"Wind\":{\"speed\":5.57,\"deg\":229.002},\"Humidity\":81.0,\"RainingProbability\":0,\"Date\":\"2016-01-30T21:00:00\",\"WeatherCode\":804,\"WeatherDescription\":\"Clouds\\r\\novercast clouds\"}],\"Code\":800,\"Precipation\":0}]";
;
            this.mockedMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).ReturnsAsync(new HttpResponseMessage { Content = new StringContent(message), StatusCode = HttpStatusCode.OK });
            var actual = await this.sut.GetDailyForecastAsync(this.request,fromDate,toDate);

            Assert.True(AssertExtenstions.DeepEqual(expected, JsonConvert.SerializeObject(actual)));

        }
    }
}
