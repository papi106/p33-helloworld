// <copyright file="Startup.cs" company="Principal 33">
// Copyright (c) Principal 33. All rights reserved.
// </copyright>

using HelloWorldWebApp.Controllers;
using Microsoft.Extensions.Configuration;

namespace HelloWorldWebApp
{
    public class WeatherConfigurationSettings : IWeatherConfigurationSettings
    {
        public WeatherConfigurationSettings(IConfiguration configurationSettings)
        {
            Longitude = configurationSettings["WeatherForecast:Longitude"];
            Latitude = configurationSettings["WeatherForecast:Latitude"];
            ApiKey = configurationSettings["WeatherForecast:ApiKey"];
        }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public string ApiKey { get; set; }
    }
}