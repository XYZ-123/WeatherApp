using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.Models
{
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json.Serialization;
    using Newtonsoft.Json;
    public class Wind
    {
        [JsonProperty(PropertyName = "speed")]
        public double Speed { get; set; }

        [JsonProperty(PropertyName = "deg")]
        public int Degree { get; set; }
    }

}
