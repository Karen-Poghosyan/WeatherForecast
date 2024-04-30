using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Core.Abstractions.DAL.Repositories;
using WeatherForecast.Core.Entities;
using WeatherForecast.DAL.DataAccess;
using WeatherForecast.DAL.Repositories.Base;

namespace WeatherForecast.DAL.Repositories
{
    public class WeatherObservationRepository : BaseRepository<WeatherObservation>, IWeatherObservationRepository
    {
        public WeatherObservationRepository(DataContext context) : base(context)
        {
        }
    }
}
