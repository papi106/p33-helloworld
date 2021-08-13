using System;
using System.Collections.Generic;
using System.Linq;
using HelloWorldWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace HelloWorldWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly string longitude = "46.7700";
        private readonly string latitude = "23.580";
        private readonly string apiKey = "35590ffc32b328af10511b2e696457d6";

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

        public IEnumerable<DailyWeather> ConvertResponseToWeatherRecordList(string content)
        {
            var json = JObject.Parse(content);

            List<DailyWeather> result = new List<DailyWeather>();
            var jsonArray = json["daily"].Take(7);

            foreach (var item in jsonArray)
            {
                DailyWeather dailyWeatherRecord = new DailyWeather(30, WeatherType.Sweltering, DateTime.Now);
                long unixDateTime = item.Value<long>("dt");

                dailyWeatherRecord.Day = DateTimeOffset.FromUnixTimeSeconds(unixDateTime).DateTime.Date;
                dailyWeatherRecord.Temperature = item["temp"].Value<float>("day") - 272.88f;

                string weatherType = item["weather"][0].Value<string>("description");
                dailyWeatherRecord.Type = ConvertToWeatherType(weatherType);

                result.Add(dailyWeatherRecord);
            }

            return result;
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

                default:
                    throw new Exception($"Unkown weather type - {weatherType}.");
            }
        }
    }
}
