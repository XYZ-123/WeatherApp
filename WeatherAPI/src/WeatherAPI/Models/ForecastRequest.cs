using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.Models
{
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;

    public class ForecastRequest
    {
        [JsonProperty(PropertyName = "units")]
        public MeasureUnits Units{ get; set; }

        [JsonProperty(PropertyName = "location")]
        public GeoLocation Location { get; set; }
    }
}
