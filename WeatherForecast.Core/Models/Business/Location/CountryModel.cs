using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Core.Models.Business.Location
{
    #nullable enable
    public class CountryModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? LocalName { get; set; }
        public string? WebCode { get; set; }
        public string? Region { get; set; }
        public string? Continent { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double SurfaceArea { get; set; }
        public int Population { get; set; }
    }
}
