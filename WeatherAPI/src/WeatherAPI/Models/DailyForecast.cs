using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.Models
{
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;

    public class DailyForecast
    {
        public DailyForecast()
        {
            Hourly = new List<HourlyForecast>();
        }
        [JsonProperty(PropertyName = "location")]
        public GeoLocation Location { get; set; }

        public double MinTemprature { get; set; }

        public double MaxTemprature { get; set; }

        public MeasureUnits Units{ get; set; }

        public double Pressure { get; set; }

        public double Humidity { get; set; }

        public int RainingProbability { get; set; }

        public DateTime Date { get; set; }

        public List<HourlyForecast> Hourly { get; set; } 

        public int Code { get; set; }

        public Precipation Precipation { get; set; }
    }
}
