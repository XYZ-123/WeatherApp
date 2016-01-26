using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI
{
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Mvc.Controllers;
    using Microsoft.AspNet.Mvc.Infrastructure;

    using WeatherAPI.Controllers;
    using WeatherAPI.Services;
    using WeatherAPI.Services.OpenWeatherMapService;

    public class SwitchControllerActivator : DefaultControllerActivator
    {
        
        public override object Create(ActionContext context, Type controllerType)
        {
            if (controllerType == typeof(ForecastController))
            {
                
                var serviceName = context.HttpContext.Request.Query["weatherprovider"];
                var service = GetWeatherService(serviceName);

                return new ForecastController(service);
            }
            return base.Create(context, controllerType);

        }



        public IWeatherService GetWeatherService(string serviceName)
        {
            switch (serviceName)
            {
                case "OpenWeather": return new OpenWeatherMapService();
                default:
                    return null;
            }
        }

        public SwitchControllerActivator(ITypeActivatorCache typeActivatorCache)
            : base(typeActivatorCache)
        {
        }
    }
}
