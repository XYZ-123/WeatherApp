namespace WeatherAPI.Services.OpenWeatherMapService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using WeatherAPI.Models;

    public class OpenWeatherMapService : IWeatherService
    {
        private string appid = "2affcc7675263d5ca51b7d4da8a13bd6";

        private HttpClient httpClient { get; }

        public OpenWeatherMapService(): this(new HttpClient())
        {
        }

        public OpenWeatherMapService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<CurrentWeather> GetCurrentWeatherAsync(ForecastRequest forecastRequest)
        {

            var url = GetRequestUrl(forecastRequest, "weather");
            var response = await httpClient.GetStringAsync(url);

            var forecast = JsonConvert.DeserializeObject<CurrentWeatherResponse>(response);
            var currentWeather = forecast.ToCurrentWeather();
            currentWeather.Units = forecastRequest.Units;
            return currentWeather;
        }

        public async Task<List<DailyForecast>> GetDailyForecastAsync(ForecastRequest forecastRequest, DateTime fromDate, DateTime toDate)
        {
            var url = GetRequestUrl(forecastRequest, "forecast");
            var response = await httpClient.GetStringAsync(url);

            var forecast = JsonConvert.DeserializeObject<DailyForecastResponse>(response);
            var dailyForecasts = forecast.ToDailyForecasts();
            dailyForecasts.Select(df => df.Units = forecastRequest.Units);
            return dailyForecasts.Where(df => df.Date >= fromDate && df.Date<=toDate).ToList();
        }

        private string GetRequestUrl(ForecastRequest forecastRequest, string api)
        {
            return $"http://api.openweathermap.org/data/2.5/{api}?q="
                   + $"{forecastRequest.Location.City},"
                   + $"{forecastRequest.Location.Country}"
                   + $"&appid={this.appid}" 
                   + $"&units={this.GetUnits(forecastRequest.Units)}";
        }

        private string GetUnits(MeasureUnits units)
        {
            switch (units)
            {
                case MeasureUnits.Celsium:
                    return "metric";
                case MeasureUnits.Fahrenheit:
                    return "imperial";
                default:
                    return "metric";
            }
        }
    }
}
