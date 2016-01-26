using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.Services.OpenWeatherMapService
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using WeatherAPI.Models;

    public class CurrentWeatherResponse
    {
        public Coordinates coord { get; set; }

        public List<Weather> weather { get; set; }

        [JsonProperty(PropertyName="base")]
        public string internalStation { get; set; }

        public Main main { get; set; }

        public Wind wind { get; set; }

        public Clouds clouds { get; set; }

        [JsonProperty(PropertyName = "dt")]
        public int TimeOfDay { get; set; }

        public Sys sys { get; set; }

        public int id { get; set; }

        public string name { get; set; }

        public int cod { get; set; }

        public Dictionary<string, string> rain { get; set; }
        public Dictionary<string, string> snow { get; set; }

        public CurrentWeather ToCurrentWeather()
        {
            var currentWeather = new CurrentWeather();

            currentWeather.CurrentTemprature = this.main.temp;
            currentWeather.Humidity = this.main.humidity;
            currentWeather.Pressure = this.main.pressure;
            currentWeather.WeatherDescription = string.Concat(weather[0].main, Environment.NewLine, weather[0].description);
            currentWeather.Date = DateTime.Today + TimeSpan.FromTicks(this.TimeOfDay);
            currentWeather.Wind = this.wind;
            currentWeather.Location = new GeoLocation { City = this.name };

            return currentWeather;
        }

        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class Main
        {
            public double temp { get; set; }
            public int pressure { get; set; }
            public int humidity { get; set; }
            public double temp_min { get; set; }
            public double temp_max { get; set; }
        }


        public class Clouds
        {
            public int all { get; set; }
        }
        
        public class Sys
        {
            public int type { get; set; }
            public int id { get; set; }
            public double message { get; set; }
            public string country { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
        }
    }
}
