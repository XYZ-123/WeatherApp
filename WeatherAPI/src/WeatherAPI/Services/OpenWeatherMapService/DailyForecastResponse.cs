using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.Services.OpenWeatherMapService
{
    using System.Globalization;

    using Newtonsoft.Json;

    using WeatherAPI.Models;

    public class DailyForecastResponse
    {
        public City city { get; set; }
        public string cod { get; set; }
        public double message { get; set; }
        public int cnt { get; set; }

        [JsonProperty(PropertyName = "list")]
        public List<PeriodicForecast> PeriodicForecasts { get; set; }

        public List<DailyForecast> ToDailyForecasts()
        {
            var list = new List<DailyForecast>();

            var dates = PeriodicForecasts.Select(s => DateTime.ParseExact(s.TimeStampString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).Date).Distinct();

            var forecastsByDay = PeriodicForecasts.GroupBy(
                forecast => DateTime.Now.Date + TimeSpan.FromTicks(forecast.TimeStamp),
                forecast => forecast,
                (date, forecasts) => new { Date = date, Forecasts = forecasts });

            foreach (var date in dates)
            {
                var dailyForecast = new DailyForecast();
                var forecastsForDate = PeriodicForecasts.Where(f=> DateTime.ParseExact(f.TimeStampString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).Date == date).ToList();
                dailyForecast.Date = date;
                dailyForecast.MinTemprature = forecastsForDate
                                                .Select(forecast => forecast.main.temp_min)
                                                .Min();
                dailyForecast.MaxTemprature = forecastsForDate
                                                .Select(forecast => forecast.main.temp_max)
                                                .Max();
                dailyForecast.Pressure = forecastsForDate.Select(forecast => forecast.main.pressure).Average();
                dailyForecast.Humidity = forecastsForDate.Select(forecast => forecast.main.humidity).Average();

                var statuses = forecastsForDate.GroupBy(forecast => forecast.weather[0].id, forecast => forecast.weather[0].id, (i, ints) => new {Code = i, Count = ints.Count()});
                dailyForecast.Code = statuses.OrderByDescending(f => f.Count).First().Code;
                dailyForecast.Hourly.AddRange(forecastsForDate.Select(f => f.ToHourlyForecast()));

                list.Add(dailyForecast);
            }

            return list;
        } 
        public class City
        {
            public int id { get; set; }
            public string name { get; set; }
            public Coordinates coord { get; set; }
            public string country { get; set; }
            public int population { get; set; }
        }

        public class Main
        {
            public double temp { get; set; }
            public double temp_min { get; set; }
            public double temp_max { get; set; }
            public double pressure { get; set; }
            public double sea_level { get; set; }
            public double grnd_level { get; set; }
            public int humidity { get; set; }
            public double temp_kf { get; set; }
        }

        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class Clouds
        {
            public int all { get; set; }
        }
    
        public class PeriodicForecast
        {
            [JsonProperty(PropertyName ="dt")]
            public int TimeStamp { get; set; }
            public Main main { get; set; }
            public List<Weather> weather { get; set; }
            public Clouds clouds { get; set; }
            public Wind wind { get; set; }

            [JsonProperty(PropertyName = "dt_txt")]
            public string TimeStampString { get; set; }
            public Dictionary<string,string> rain { get; set; }
            public Dictionary<string, string> snow { get; set; }

            public HourlyForecast ToHourlyForecast()
            {
                var hourlyForecast = new HourlyForecast();
                hourlyForecast.Date = DateTime.ParseExact(
                    TimeStampString,
                    "yyyy-MM-dd HH:mm:ss",
                    CultureInfo.InvariantCulture);
                hourlyForecast.Humidity = main.humidity;
                hourlyForecast.Pressure = main.pressure;
                hourlyForecast.Temperature = Math.Round(main.temp);
                hourlyForecast.WeatherCode = weather[0].id;
                hourlyForecast.WeatherDescription = String.Concat(
                    weather[0].main,
                    Environment.NewLine,
                    weather[0].description);
                hourlyForecast.Wind = wind;
                return hourlyForecast;
            }
        }     
    }
}

