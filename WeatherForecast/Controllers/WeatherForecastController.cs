using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Core.Abstractions.BLL;
using WeatherForecast.Core.Models.Business.Location;
using WeatherForecast.Core.Models.Business.WeatherInfo;
using WeatherForecast.Core.Models.Request;

namespace WeatherForecast.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _weatherForecastService;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
        }

        [HttpGet("getForecastByDay")]
        public async Task<IActionResult> GetForecastByDay([FromQuery] DateOnly day)
        {
            var forecast = await _weatherForecastService.GetForecastByDayAsync(day);
           
            if (forecast == null)
            {
                return NotFound();
            
            }
            return Ok(forecast);
        }

        [HttpGet("getNearestWarmestDaysForecast")]
        public async Task<IActionResult> GetNearestWarmestDaysForecast([FromBody] LocationModel locationModel)
        {
            var forecasts = await _weatherForecastService.GetNearestWarmestDaysForecastAsync(locationModel);
           
            if (forecasts == null || !forecasts.Any())
            {
                return NotFound();
            
            }
            return Ok(forecasts);
        }

        [HttpPost("addForecastByWeek")]

        public async Task<IActionResult> AddForecastByWeek([FromBody] AddWeatherByWeekRequestModel weatherByWeek)
        {
            await _weatherForecastService.AddForecastByWeekAsync(weatherByWeek);
            return Ok();
        }

        [HttpPost("addForecastByDay")]
        public async Task<IActionResult> AddForecastByDay([FromBody] AddWeatherByDayRequestModel weatherForecastByDay)
        {
            await _weatherForecastService.AddForecastByDayAsync(weatherForecastByDay);
            return Ok();
        }

        [HttpPut("updateForecastByDay")]
        public async Task<IActionResult> UpdateForecastByDay([FromBody] WeatherByDayInfoModel weatherByDay)
        {
            await _weatherForecastService.UpdateForecastByDayAsync(weatherByDay);
            return Ok();
        }

    }
}