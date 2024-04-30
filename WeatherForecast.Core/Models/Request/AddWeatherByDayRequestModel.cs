using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Core.Models.Business.WeatherInfo;

namespace WeatherForecast.Core.Models.Request
{
    public class AddWeatherByDayRequestModel
    {
        public AddWeatherByTimeRequestModel[] WeatherByHours { get; set; } = new AddWeatherByTimeRequestModel[24];
    }
}
