using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Wind
    {
        [Display(Name="speed")]
        public double Speed { get; set; }
        [Display(Name = "deg")]
        public int Degree { get; set; }
    }

}
