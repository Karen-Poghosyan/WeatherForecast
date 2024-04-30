using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Core.Models.Business.Location
{
    public class CityModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
        public string CountryId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
