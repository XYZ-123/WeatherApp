using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace WeatherAPI.Controllers
{
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Newtonsoft.Json;

    using WeatherAPI.CustomAttributes;
    using WeatherAPI.Models;
    using WeatherAPI.Services;

    [Route("api/[controller]")]
    public class ForecastController : Controller
    {
        private IWeatherService _weatherService;
        public ForecastController(IWeatherService weatherService)
        {
            if(weatherService == null)
                throw new ArgumentNullException(nameof(weatherService));

           _weatherService = weatherService;
        }

        [HttpPost]
        [Route("currentweather")]
        public async Task<IActionResult> GetCurrentForecast([FromBody] ForecastRequest request)
        {
            var result = await _weatherService.GetCurrentWeatherAsync(request);
            return new JsonResult(result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [HttpPost]
        [Route("dailyforecast/{fromDate}/{toDate}")]
        public async Task<IActionResult> GetForecastRange([FromBody] ForecastRequest request, DateTime fromDate, DateTime toDate)
        {
            var result = await _weatherService.GetDailyForecastAsync(request, fromDate, toDate);
            return new JsonResult(result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

        }
    }
}
