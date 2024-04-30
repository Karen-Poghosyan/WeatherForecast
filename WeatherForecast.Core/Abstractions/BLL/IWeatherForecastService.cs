using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Core.Models.Business.Location;
using WeatherForecast.Core.Models.Business.WeatherInfo;
using WeatherForecast.Core.Models.Request;

namespace WeatherForecast.Core.Abstractions.BLL
{
    public interface IWeatherForecastService
    {
        public Task<WeatherByDayInfoModel> GetForecastByDayAsync(DateOnly day);
        public Task<IEnumerable<WeatherByDayInfoModel>> GetNearestWarmestDaysForecastAsync(LocationModel locationModel);
        public Task AddForecastByWeekAsync(AddWeatherByWeekRequestModel weatherByWeek);
        public Task AddForecastByDayAsync(AddWeatherByDayRequestModel weatherForecastByDay);
        public Task UpdateForecastByDayAsync(WeatherByDayInfoModel weatherByDay);
        public Task RemoveForecastsByDateAsync(DateTime date);

    }
}
