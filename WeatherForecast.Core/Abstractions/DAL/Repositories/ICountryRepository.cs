using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Core.Abstractions.DAL.Repositories.Base;
using WeatherForecast.Core.Entities;

namespace WeatherForecast.Core.Abstractions.DAL.Repositories
{
    public interface ICountryRepository : IBaseRepository<Country>
    {
    }
}
