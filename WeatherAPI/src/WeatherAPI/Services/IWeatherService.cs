using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.Services
{
    using WeatherAPI.Models;

    public interface IWeatherService
    {
        Task<CurrentWeather> GetCurrentWeatherAsync(ForecastRequest forecastRequest);
        Task<List<DailyForecast>> GetDailyForecastAsync(ForecastRequest forecastRequest, DateTime fromDate, DateTime toDate);

    }
}
