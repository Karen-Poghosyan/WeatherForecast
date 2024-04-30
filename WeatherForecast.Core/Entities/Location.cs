using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Core.Entities
{
    public class Location : BaseEntity
    {
        public long Id { get; set; }
        public string CountryId { get; set; }
        public virtual Country Country { get; set; }
        public int? StateId { get; set; }
        public virtual State State { get; set; }
        public int? CityId { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<WeatherObservation> WeatherForecasts { get; set; }  
    }

}
