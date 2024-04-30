using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Core.Enums;
using WeatherForecast.Core.Models.Business.Location;

namespace WeatherForecast.Core.Models.Business.WeatherInfo
{
    public class WeatherByDayInfoModel
    {
        public LocationModel Location { get; set; }
        public DateOnly Date { get; set; }
        public double MaxTemperatureInCel { get; set; }
        public double MinTemperatureInCel { get; set; }
        public double AverageTemperatureInCel { get; set; }
        public double? AverageWindSpeed { get; set; }
        public WeatherCondition AverageCondition { get; set; }
        public List<WeatherByTimeInfoModel> WeatherByHours { get; set; }
    }
}
