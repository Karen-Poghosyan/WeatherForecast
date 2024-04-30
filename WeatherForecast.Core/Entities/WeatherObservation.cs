using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Core.Enums;
using WeatherForecast.Core.Models.Business.Location;

namespace WeatherForecast.Core.Entities
{
    public class WeatherObservation : BaseEntity
    {
        public long Id { get; set; }
        public long LocationId { get; set; }
        public DateTimeOffset DateTime { get; set; } 
        public double TemperatureInCel { get; set; }
        public double? WindSpeed { get; set; }
        public WeatherCondition Condition { get; set; }

        public virtual Location Location { get; set; }
    }
}
