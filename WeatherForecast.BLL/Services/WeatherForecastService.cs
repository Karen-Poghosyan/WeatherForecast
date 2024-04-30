using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Core.Abstractions.BLL;
using WeatherForecast.Core.Abstractions.DAL.Repositories;
using WeatherForecast.Core.Entities;
using WeatherForecast.Core.Helpers;
using WeatherForecast.Core.Models.Business.Location;
using WeatherForecast.Core.Models.Business.WeatherInfo;
using WeatherForecast.Core.Models.Request;

namespace WeatherForecast.BLL.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IWeatherObservationRepository _weatherRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly ILogger<IWeatherForecastService> _logger;
        public WeatherForecastService(IWeatherObservationRepository weatherRepository, ILocationRepository locationRepository, ILogger<IWeatherForecastService> logger)
        {
            _logger = logger;
            _weatherRepository = weatherRepository;
            _locationRepository = locationRepository;
        }

        public async Task AddForecastByDayAsync(AddWeatherByDayRequestModel weatherForecastByDay)
        {
            try
            {
                if (weatherForecastByDay.WeatherByHours.Any())
                {
                    var weatherForecastCheckModel = weatherForecastByDay.WeatherByHours.FirstOrDefault();

                    var actualDate = weatherForecastCheckModel.DateTime.Date;
                    var actualLocation = weatherForecastCheckModel.Location;

                    var distinctLocations = weatherForecastByDay.WeatherByHours.Select(t => t.Location).Distinct().ToList();
                    var distinctDates = weatherForecastByDay.WeatherByHours.Select(t => t.DateTime.Date).Distinct().ToList();

                    if (distinctLocations.Count() > 1 || distinctDates.Count() > 1)
                    {
                        throw new InvalidDataException("There is weather forecast for different dates or different locations");
                    }

                    var inAllowedDateInterval = (actualDate >= DateTime.Today && actualDate.Date <= DateTime.Now.AddDays(6));

                    if (!inAllowedDateInterval)
                    {
                        throw new InvalidDataException("The valid date range for adding a weather forecast is 7 days, starting from today.");
                    }


                    var existingWeather = _weatherRepository.Find(w => w.DateTime.Date == actualDate &&
                                                        w.Location.CountryId == actualLocation.Country.Id &&
                                                        (w.Location.StateId == actualLocation.State.Id || (w.Location.StateId == null && actualLocation.State == null)) &&
                                                        (w.Location.CityId == actualLocation.City.Id || (w.Location.CityId == null && actualLocation.City == null))).FirstOrDefault();

                    if (existingWeather != null)
                    {
                        throw new InvalidDataException("The weather forecast for that day already exists");
                    }

                    List<WeatherObservation> weatherForecast = new();
                    Location location;

                    var isLocationExists = await _locationRepository.CheckIfExsistsAsync(actualLocation);

                    if (!isLocationExists)
                    {
                        location = await _locationRepository.CreateLocationByModelAsync(actualLocation);
                    }
                    else
                    {
                        location = await _locationRepository.GetLocationByModelAsync(actualLocation);
                    }

                    foreach (var weatherByHour in weatherForecastByDay.WeatherByHours)
                    {
                        var weatherObservation = new WeatherObservation
                        {

                            CreatedDate = DateTime.UtcNow,
                            IsActive = true,
                            Condition = weatherByHour.Condition,
                            DateTime = weatherByHour.DateTime,
                            Location = location,
                            TemperatureInCel = weatherByHour.TemperatureInCel,
                            WindSpeed = weatherByHour.WindSpeed,
                            LocationId = location.Id
                        };
                        weatherForecast.Add(weatherObservation);
                    }

                    if (weatherForecast.Any())
                    {
                        await _weatherRepository.InsertRangeAsync(weatherForecast);
                        await _weatherRepository.SaveChangesAsync();
                    }
                }
            }catch(Exception ex) 
            {
                _logger.LogError($"WeatherForecastService -- AddForecastByDayAsync -- {ex.Message}");
                _logger.LogError($"WeatherForecastService -- AddForecastByDayAsync -- {ex.InnerException}");
                _logger.LogError($"WeatherForecastService -- AddForecastByDayAsync -- {ex.StackTrace}");
            }
        }

        public async Task AddForecastByWeekAsync(AddWeatherByWeekRequestModel weatherByWeek)
        {
            foreach (var dayRequest in weatherByWeek.WeatherByDays)
            {
                await AddForecastByDayAsync(dayRequest);
            }
        }

        public async Task<WeatherByDayInfoModel> GetForecastByDayAsync(DateOnly day)
        {
            var weatherForecasts = await _weatherRepository.Find(w => w.DateTime.Day == day.Day).ToListAsync();

            WeatherByDayInfoModel forecastByDay;

            if (weatherForecasts != null && weatherForecasts.Any())
            {
                var orederedWeatherForecasts = weatherForecasts.OrderByDescending(w => w.DateTime).ToList();

                var forecastModel = orederedWeatherForecasts.FirstOrDefault();
                
                var locationModel = await _locationRepository.GetLocationModelAsync(forecastModel.Location);

                var weatherConditions = orederedWeatherForecasts.Select(w => w.Condition).ToList();

                var averageWeatherCondition = WeatherHelper.CalculateAverageCondition(weatherConditions);

                var averageWindSpeed = orederedWeatherForecasts.Select(w => w.WindSpeed).Average();

                var averageTemperatureC = orederedWeatherForecasts.Select( w => w.TemperatureInCel).Average();

                var maxTemperatureC = orederedWeatherForecasts.Select(w => w.TemperatureInCel).Max();
                
                var minTemperatureC = orederedWeatherForecasts.Select(w => w.TemperatureInCel).Min();

                var forecastByHours = new List<WeatherByTimeInfoModel>();

                foreach (var orederedWeatherForecast in orederedWeatherForecasts)
                {
                    var forecastByHour = new WeatherByTimeInfoModel
                    {
                        Location = locationModel,
                        Condition = orederedWeatherForecast.Condition,
                        TemperatureInCel = orederedWeatherForecast.TemperatureInCel,
                        DateTime = orederedWeatherForecast.DateTime.DateTime,
                        WindSpeed = orederedWeatherForecast.WindSpeed,
                    };

                    forecastByHours.Add(forecastByHour);
                }

                forecastByDay = new WeatherByDayInfoModel()
                {
                    Date = new DateOnly(forecastModel.DateTime.Year, forecastModel.DateTime.Month, forecastModel.DateTime.Day),
                    Location = locationModel,
                    AverageCondition= averageWeatherCondition,
                    AverageWindSpeed = averageWindSpeed,
                    AverageTemperatureInCel = averageTemperatureC,
                    MaxTemperatureInCel = maxTemperatureC,
                    MinTemperatureInCel = minTemperatureC,
                    WeatherByHours = forecastByHours
                };

                return forecastByDay;
            }
            return null;
        }

        public async Task<IEnumerable<WeatherByDayInfoModel>> GetNearestWarmestDaysForecastAsync(LocationModel locationModel)
        {
            var today = DateTime.Today;

            var location = await _locationRepository.GetLocationByModelAsync(locationModel);

            var futureForecasts = await _weatherRepository.Find(w => w.DateTime.Date >= today && w.DateTime.Date <= today.AddDays(6) &&
                                                            w.Location.City == location.City &&
                                                            w.Location.State == location.State &&
                                                            w.Location.Country == location.Country)
                                                        .GroupBy(w => w.DateTime.Date)
                                                        .Select(g => new WeatherByDayInfoModel
                                                        {
                                                            Date = new DateOnly(g.Key.Year, g.Key.Month, g.Key.Day),
                                                            MaxTemperatureInCel = g.Max(w => w.TemperatureInCel),
                                                            MinTemperatureInCel = g.Min(w => w.TemperatureInCel),
                                                            AverageTemperatureInCel = g.Average(w => w.TemperatureInCel),
                                                            WeatherByHours = g.Select(w => new WeatherByTimeInfoModel
                                                            {
                                                                DateTime = w.DateTime.DateTime,
                                                                Location = locationModel,
                                                                TemperatureInCel = w.TemperatureInCel,
                                                                WindSpeed = w.WindSpeed,
                                                                Condition = w.Condition
                                                            }).ToList()
                                                        })
                                                        .ToListAsync();

            var nearestWarmestDays = new List<WeatherByDayInfoModel>();

            foreach (var forecast in futureForecasts)
            {
                var nextDay = today;

                while (true)
                {
                    nextDay = nextDay.AddDays(1);

                    if (!futureForecasts.Any(f => f.Date.Day == nextDay.Day))
                    {
                        break;
                    }

                    var nextDayForecast = futureForecasts.First(f => f.Date.Day == nextDay.Day);
                    if (nextDayForecast.AverageTemperatureInCel > forecast.AverageTemperatureInCel)
                    {
                        nearestWarmestDays.Add(nextDayForecast);
                        break;
                    }
                }
            }

            return nearestWarmestDays; 
        }

        public async Task UpdateForecastByDayAsync(WeatherByDayInfoModel weatherByDay)
        {
            try
            {
                if (weatherByDay.WeatherByHours.Any())
                {
                    var weatherForecastCheckModel = weatherByDay.WeatherByHours.FirstOrDefault();

                    var actualDate = weatherForecastCheckModel.DateTime.Date;
                    var actualLocation = weatherForecastCheckModel.Location;

                    var distinctLocations = weatherByDay.WeatherByHours.Select(t => t.Location).Distinct().ToList();
                    var distinctDates = weatherByDay.WeatherByHours.Select(t => t.DateTime.Date).Distinct().ToList();

                    if (distinctLocations.Count() > 1 || distinctDates.Count() > 1)
                    {
                        throw new InvalidDataException("There is weather forecast for different dates or different locations");
                    }


                    var existingWeather = await _weatherRepository.Find(w => w.DateTime.Date == actualDate &&
                                                            w.Location.CountryId == actualLocation.Country.Id &&
                                                            (w.Location.StateId == actualLocation.State.Id || (w.Location.StateId == null && actualLocation.State == null)) &&
                                                            (w.Location.CityId == actualLocation.City.Id || (w.Location.CityId == null && actualLocation.City == null))).ToListAsync();

                    if (existingWeather == null || !existingWeather.Any())
                    {
                        throw new InvalidDataException("The weather forecast for that day does not exist");
                    }

                    foreach (var existingRecord in existingWeather)
                    {
                        var updatedRecord = weatherByDay.WeatherByHours.FirstOrDefault(w => w.DateTime == existingRecord.DateTime && w.Location.Equals(actualLocation));
                        if (updatedRecord != null)
                        {
                            existingRecord.TemperatureInCel = updatedRecord.TemperatureInCel;
                            existingRecord.WindSpeed = updatedRecord.WindSpeed;
                            existingRecord.Condition = updatedRecord.Condition;
                            existingRecord.UpdatedDate = DateTime.UtcNow;
                        }
                    }

                    await _weatherRepository.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"WeatherForecastService -- UpdateForecastByDayAsync -- {ex.Message}");
                _logger.LogError($"WeatherForecastService -- UpdateForecastByDayAsync -- {ex.InnerException}");
                _logger.LogError($"WeatherForecastService -- UpdateForecastByDayAsync -- {ex.StackTrace}");
            }
        }

        public async Task RemoveForecastsByDateAsync(DateTime date)
        {
            var forecastsToRemove = await _weatherRepository.Find(w => w.DateTime.Date == date.Date &&
                                                                w.DateTime.TimeOfDay >= date.TimeOfDay).ToListAsync();

            if (forecastsToRemove.Any())
            {
                _weatherRepository.DeleteRange(forecastsToRemove);
                await _weatherRepository.SaveChangesAsync();
            }
        }

    }
}
