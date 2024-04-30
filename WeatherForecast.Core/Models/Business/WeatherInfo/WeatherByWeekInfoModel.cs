using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Core.Enums;
using WeatherForecast.Core.Models.Business.Location;

namespace WeatherForecast.Core.Models.Business.WeatherInfo
{
    public class WeatherByWeekInfoModel
    {
        public LocationModel Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double MaxTemperatureInCel { get; set; }
        public double MinTemperatureInCel { get; set; }
        public double AverageTemperatureInCel { get; set; }
        public double? AverageWindSpeed { get; set; }
        public WeatherCondition AverageCondition { get; set;}
        public List<WeatherByDayInfoModel> WeatherByDays { get; set; }
    }
}
