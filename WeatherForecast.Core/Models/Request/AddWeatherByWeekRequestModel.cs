using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Core.Models.Business.Location;
using WeatherForecast.Core.Models.Business.WeatherInfo;

namespace WeatherForecast.Core.Models.Request
{
    public class AddWeatherByWeekRequestModel
    {
        public AddWeatherByDayRequestModel[] WeatherByDays { get; set; } = new AddWeatherByDayRequestModel[7];
    }
}
