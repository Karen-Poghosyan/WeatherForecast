namespace WeatherForecast.Core.Models.Business.Location
{
    public class LocationModel
    {
        public CountryModel Country { get; set; }   
        public StateModel State { get; set; }
        public CityModel City { get; set; }
    }
}