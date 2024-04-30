#nullable disable

using System;
using System.Collections.Generic;

namespace WeatherForecast.Core.Entities
{
    public partial class City : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
        public string CountryId { get; set; } 
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public virtual State State { get; set; } 
        public virtual ICollection<Location> Locations { get; set; }
    }
}
