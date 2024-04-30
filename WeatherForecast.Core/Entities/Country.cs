using System;
using System.Collections.Generic;

namespace WeatherForecast.Core.Entities
{ 
    #nullable enable
    public partial class Country : BaseEntity
    {
        public Country()
        {
            States = new HashSet<State>();
        }

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
        public virtual ICollection<State> States { get; set; }
        public virtual ICollection<Location>? Locations { get; set; }
    }
}
