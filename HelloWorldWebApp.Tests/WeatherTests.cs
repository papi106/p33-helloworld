using HelloWorldWebApp.Controllers;
using HelloWorldWebApp.Models;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
            string content = LoadJSONFromResource() ;
            var weatherConfigurationMoq = new Mock<IWeatherConfigurationSettings>();
            WeatherController weatherController = new WeatherController(weatherConfigurationMoq.Object);

            //Act
            var result = weatherController.ConvertResponseToWeatherRecordList(content);

            //Assert
            Assert.Equal(7, result.Count());

            var firstDay = result.First();
            Assert.Equal(new DateTime(2021, 8, 12), firstDay.Day);
            Assert.Equal(24.730010986328125, firstDay.Temperature);
            Assert.Equal(WeatherType.Cloudy, firstDay.Type);
        }

        [Fact]
        private string LoadJSONFromResource()
        {
            var assembly = this.GetType().Assembly;
            var assemblyName = assembly.GetName().Name;
            string resourceName = $"{ assemblyName}.TestData.ContentFromWeaherAPI.json";
            var resourceStream = assembly.GetManifestResourceStream(resourceName);
            using (var tr = new StreamReader(resourceStream))
            {
                return tr.ReadToEnd();
            }
        }
    }
}
