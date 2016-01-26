using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.Models
{
    using Newtonsoft.Json;

    public class CurrentWeather
    {
        [JsonProperty(PropertyName = "location")]
        public GeoLocation Location { get; set; }
        public double CurrentTemprature { get; set; }

        public MeasureUnits Units { get; set; }

        public int Pressure { get; set; }

        public int Humidity { get; set; }

        public int RainingProbability { get; set; }

        public DateTime Date { get; set; }

        public string WeatherDescription { get; set; }
        public Wind Wind { get; set; }
        public Precipation Precipation { get; set; }
    }
}
