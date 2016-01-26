﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WeatherAPI
{
    using Microsoft.AspNet.Mvc.Controllers;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    using WeatherAPI.CustomAttributes;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            //services.Replace(ServiceDescriptor.Instance(typeof(IControllerActivator), new Switchb))

            services.AddMvc().AddMvcOptions(
                options =>
                    {
                        options.ModelBinders.Insert(0, new DateTimeBinder());
                    });

            services.Replace(ServiceDescriptor.Transient(typeof(IControllerActivator), typeof(SwitchControllerActivator)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            app.UseIISPlatformHandler();

            app.UseStaticFiles();
            
            app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args) =>
            WebApplication.Run<Startup>(args);
        
    }
}