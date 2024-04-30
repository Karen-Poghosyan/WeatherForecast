using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Core.Enums;
using WeatherForecast.Core.Models.Business.Location;

namespace WeatherForecast.Core.Models.Request
{
    public class AddWeatherByTimeRequestModel
    {
        public LocationModel Location { get; set; }
        public DateTime DateTime { get; set; }
        public double TemperatureInCel { get; set; }
        public double? WindSpeed { get; set; }
        public WeatherCondition Condition { get; set; }
    }
}
