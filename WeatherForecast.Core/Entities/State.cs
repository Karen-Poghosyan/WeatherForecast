#nullable disable

using System;
using System.Collections.Generic;

namespace WeatherForecast.Core.Entities
{
    public partial class State : BaseEntity
    {
        public State()
        {
            Cities = new HashSet<City>();
        }

        public int Id { get; set; } 
        public string Name { get; set; } = null!;
        public string CountryId { get; set; } = null!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public virtual Country Country { get; set; } 
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
    }
}
