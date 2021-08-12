using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldWebApp.Models
{
    public class DailyWeather
    {
        public DailyWeather(float temperature, WeatherType type, DateTime day)
        {
            Temperature = temperature;
            Type = type;
            Day = day;
        }

        public float Temperature { get; set; }

        public WeatherType Type { get; set; }

        public DateTime Day { get; set; }

    }
}
