using System;
using System.Collections.Generic;
using System.Linq;
using HelloWorldWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RestSharp;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace HelloWorldWebApp.Controllers
{
    /// <summary>
    /// Fetch data from Weather API.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        public const float KELVIN_CONST = 273.15f;

        private readonly string longitude;
        private readonly string latitude;
        private readonly string apiKey;

        private readonly IWeatherConfigurationSettings settings;

        public WeatherController(IWeatherConfigurationSettings configurationSettings)
        {
            settings = configurationSettings;

            longitude = configurationSettings.Longitude;
            latitude = configurationSettings.Latitude;
            apiKey = configurationSettings.ApiKey;
        }

        // GET: api/<WeatherController>
        [HttpGet]
        public IEnumerable<DailyWeather> Get()
        {
            var client = new RestClient($"https://api.openweathermap.org/data/2.5/onecall?lat={latitude}&lon={longitude}&exclude=hourly,minutely&appid={apiKey}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return ConvertResponseToWeatherRecordList(response.Content);
        }

        [NonAction]
        public IEnumerable<DailyWeather> ConvertResponseToWeatherRecordList(string content)
        {
            var json = JObject.Parse(content);
            var jsonArray = json["daily"].Take(7);

            return jsonArray.Select(CreateDailyWeatherFromJToken);
        }

        // GET api/<WeatherController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // DELETE api/<WeatherController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private WeatherType ConvertToWeatherType(string weatherType)
        {
            switch (weatherType)
            {
                case "few clouds":
                    return WeatherType.Cloudy;

                case "light rain":
                    return WeatherType.LightRain;

                case "broken clouds":
                    return WeatherType.BrokenClouds;

                case "moderate rain":
                    return WeatherType.BrokenClouds;

                case "clear sky":
                    return WeatherType.ClearSky;

                default:
                    throw new Exception($"Unkown weather type - {weatherType}.");
            }
        }

        private DailyWeather CreateDailyWeatherFromJToken(JToken item)
        {
            long unixDateTime = item.Value<long>("dt");
            DateTime day = DateTimeOffset.FromUnixTimeSeconds(unixDateTime).DateTime.Date;
            float temperature = item["temp"].Value<float>("day") - KELVIN_CONST;
            string weatherType = item["weather"][0].Value<string>("description");
            WeatherType type = ConvertToWeatherType(weatherType);

            return new DailyWeather(temperature, type, day);
        }
    }
}
