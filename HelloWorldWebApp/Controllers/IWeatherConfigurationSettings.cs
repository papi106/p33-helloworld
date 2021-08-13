namespace HelloWorldWebApp.Controllers
{
    public interface IWeatherConfigurationSettings
    {
        public string Longitude { get; }

        public string Latitude { get; }

        public string ApiKey { get; }
    }
}