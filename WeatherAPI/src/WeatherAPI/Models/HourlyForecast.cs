using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.Models
{
    public class HourlyForecast
    {

        public double Temperature { get; set; }

        public MeasureUnits Units { get; set; }

        public double Pressure { get; set; }

        public Wind Wind { get; set; }

        public double Humidity { get; set; }

        public int RainingProbability { get; set; }

        public DateTime Date { get; set; }
        
        public int WeatherCode { get; set; }

        public string WeatherDescription { get; set; }
    }
}
