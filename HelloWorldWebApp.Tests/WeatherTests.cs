using HelloWorldWebApp.Controllers;
using HelloWorldWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HelloWorldWebApp.Tests
{
    public class WeatherTests
    {
        [Fact]
        public void CheckingConversion()
        {
            //Assume
            string content = "{\"lat\":46.77,\"lon\":23.58,\"timezone\":\"Europe / Bucharest\",\"timezone_offset\":10800,\"current\":{\"dt\":1628756130,\"sunrise\":1628738379,\"sunset\":1628790117,\"temp\":296.93,\"feels_like\":296.93,\"pressure\":1020,\"humidity\":60,\"dew_point\":288.71,\"uvi\":4.57,\"clouds\":20,\"visibility\":10000,\"wind_speed\":4.12,\"wind_deg\":300,\"weather\":[{\"id\":801,\"main\":\"Clouds\",\"description\":\"few clouds\",\"icon\":\"02d\"}]},\"minutely\":[{\"dt\":1628756160,\"precipitation\":0},{\"dt\":1628756220,\"precipitation\":0},{\"dt\":1628756280,\"precipitation\":0},{\"dt\":1628756340,\"precipitation\":0},{\"dt\":1628756400,\"precipitation\":0},{\"dt\":1628756460,\"precipitation\":0},{\"dt\":1628756520,\"precipitation\":0},{\"dt\":1628756580,\"precipitation\":0},{\"dt\":1628756640,\"precipitation\":0},{\"dt\":1628756700,\"precipitation\":0},{\"dt\":1628756760,\"precipitation\":0},{\"dt\":1628756820,\"precipitation\":0},{\"dt\":1628756880,\"precipitation\":0},{\"dt\":1628756940,\"precipitation\":0},{\"dt\":1628757000,\"precipitation\":0},{\"dt\":1628757060,\"precipitation\":0},{\"dt\":1628757120,\"precipitation\":0},{\"dt\":1628757180,\"precipitation\":0},{\"dt\":1628757240,\"precipitation\":0},{\"dt\":1628757300,\"precipitation\":0},{\"dt\":1628757360,\"precipitation\":0},{\"dt\":1628757420,\"precipitation\":0},{\"dt\":1628757480,\"precipitation\":0},{\"dt\":1628757540,\"precipitation\":0},{\"dt\":1628757600,\"precipitation\":0},{\"dt\":1628757660,\"precipitation\":0},{\"dt\":1628757720,\"precipitation\":0},{\"dt\":1628757780,\"precipitation\":0},{\"dt\":1628757840,\"precipitation\":0},{\"dt\":1628757900,\"precipitation\":0},{\"dt\":1628757960,\"precipitation\":0},{\"dt\":1628758020,\"precipitation\":0},{\"dt\":1628758080,\"precipitation\":0},{\"dt\":1628758140,\"precipitation\":0},{\"dt\":1628758200,\"precipitation\":0},{\"dt\":1628758260,\"precipitation\":0},{\"dt\":1628758320,\"precipitation\":0},{\"dt\":1628758380,\"precipitation\":0},{\"dt\":1628758440,\"precipitation\":0},{\"dt\":1628758500,\"precipitation\":0},{\"dt\":1628758560,\"precipitation\":0},{\"dt\":1628758620,\"precipitation\":0},{\"dt\":1628758680,\"precipitation\":0},{\"dt\":1628758740,\"precipitation\":0},{\"dt\":1628758800,\"precipitation\":0},{\"dt\":1628758860,\"precipitation\":0},{\"dt\":1628758920,\"precipitation\":0},{\"dt\":1628758980,\"precipitation\":0},{\"dt\":1628759040,\"precipitation\":0},{\"dt\":1628759100,\"precipitation\":0},{\"dt\":1628759160,\"precipitation\":0},{\"dt\":1628759220,\"precipitation\":0},{\"dt\":1628759280,\"precipitation\":0},{\"dt\":1628759340,\"precipitation\":0},{\"dt\":1628759400,\"precipitation\":0},{\"dt\":1628759460,\"precipitation\":0},{\"dt\":1628759520,\"precipitation\":0},{\"dt\":1628759580,\"precipitation\":0},{\"dt\":1628759640,\"precipitation\":0},{\"dt\":1628759700,\"precipitation\":0},{\"dt\":1628759760,\"precipitation\":0}]}";
            WeatherController weatherController = new WeatherController();

            //Act
            var result = weatherController.ConvertResponseToWeatherRecordList(content);

            //Assert
            Assert.Equal(7, result.Count());

            var firstDay = result.First();
            Assert.Equal(new DateTime(2021, 8, 12), firstDay.Day);
            Assert.Equal(296.92f, firstDay.Temperature);
            Assert.Equal(WeatherType.Cloudy, firstDay.Type);
        }
    }
}
