using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WeatherForecast.Core.Abstractions.DAL.Repositories;
using WeatherForecast.Core.Entities;
using WeatherForecast.Core.Models.Business.Location;
using WeatherForecast.DAL.DataAccess;
using WeatherForecast.DAL.Repositories.Base;

namespace WeatherForecast.DAL.Repositories
{
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        public LocationRepository(DataContext context) : base(context)
        {
        }

        public async Task<bool> CheckIfExsistsAsync(LocationModel locationModel)
        {
            var existingLocation = await Find(l =>
                                            l.CountryId == locationModel.Country.Id &&
                                            (l.StateId == locationModel.State.Id || (l.StateId == null && locationModel.State == null)) &&
                                            (l.CityId == locationModel.City.Id || (l.CityId == null && locationModel.City == null))).FirstOrDefaultAsync();

            return existingLocation != null;
        }

        public async Task<bool> CheckIfExsistsAsync(Location locationEntity)
        {
            var existingLocation = await Find(l => l == locationEntity).FirstOrDefaultAsync();

            return existingLocation != null;
        }

        public async Task<Location> CreateLocationByModelAsync(LocationModel locationModel)
        {
            var isLocationExists = await CheckIfExsistsAsync(locationModel);

            if (!isLocationExists)
            {
                var location = new Location
                {
                    CountryId = locationModel.Country.Id,
                    StateId = locationModel.State?.Id,
                    CityId = locationModel.City?.Id
                };

                await InsertAsync(location);
                await SaveChangesAsync();

                return location;
            }

            return await GetLocationByModelAsync(locationModel);
        }

        public async Task<Location> GetLocationByModelAsync(LocationModel locationModel)
        {
            var existingLocation = await Find(l =>
                                            l.CountryId == locationModel.Country.Id &&
                                            (l.StateId == locationModel.State.Id || (l.StateId == null && locationModel.State == null)) &&
                                            (l.CityId == locationModel.City.Id || (l.CityId == null && locationModel.City == null))).FirstOrDefaultAsync();

            return existingLocation;
        }

        public async Task<LocationModel> GetLocationModelAsync(Location locationEntity)
        {
            LocationModel locationModel;
            Location location;

            var locationExists = await CheckIfExsistsAsync(locationEntity);

            if (locationExists)
            {
                location = await GetSingleAsync(l => l == locationEntity);
            }
            else
            {
                await InsertAsync(locationEntity);
                await SaveChangesAsync();

                location = locationEntity;
            }


            var countryModel = new CountryModel
            {
                Id = location.Country.Id,
                Continent = location.Country.Continent,
                SurfaceArea = location.Country.SurfaceArea,
                Region = location.Country.Region,
                Name = location.Country.Name,
                LocalName = location.Country.LocalName,
                Population = location.Country.Population,
                Latitude = location.Country.Latitude,
                Longitude = location.Country.Longitude,
                WebCode = location.Country.WebCode
            };

            var stateModel = new StateModel
            {
                Id = location.State.Id,
                Name = location.State.Name,
                Latitude = location.State.Latitude,
                Longitude = location.State.Longitude,
                CountryId = location.State.CountryId
            };

            var cityModel = new CityModel
            {
                Id = location.City.Id,
                Name = location.City.Name,
                Latitude = location.City.Latitude,
                Longitude = location.City.Longitude,
                CountryId = location.City.CountryId,
                StateId = location.City.StateId
            };

            locationModel = new LocationModel
            {
                Country = countryModel,
                State = stateModel,
                City = cityModel
            };

            return locationModel;

        }
    }
}
