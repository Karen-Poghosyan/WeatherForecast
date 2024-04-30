using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Core.Abstractions.DAL.Repositories.Base;
using WeatherForecast.Core.Entities;
using WeatherForecast.Core.Models.Business.Location;

namespace WeatherForecast.Core.Abstractions.DAL.Repositories
{
    public interface ILocationRepository : IBaseRepository<Location>
    {
        public Task<bool> CheckIfExsistsAsync(LocationModel locationModel);
        public Task<bool> CheckIfExsistsAsync(Location locationEntity);
        public Task<Location> CreateLocationByModelAsync(LocationModel locationModel);
        public Task<Location> GetLocationByModelAsync(LocationModel locationModel);
        public Task<LocationModel> GetLocationModelAsync(Location locationEntity);
    }
}
