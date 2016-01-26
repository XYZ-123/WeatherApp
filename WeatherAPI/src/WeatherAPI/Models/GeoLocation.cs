using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.Models
{
    using System.ComponentModel.DataAnnotations;

    public class GeoLocation
    {
        [Display(Name="city")]
        public string City { get; set; }

        [Display(Name="country")]
        public string Country { get; set; }
    }
}
