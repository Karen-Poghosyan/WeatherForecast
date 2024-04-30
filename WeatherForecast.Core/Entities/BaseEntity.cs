using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Core.Entities
{
    public class BaseEntity
    {
       public bool IsActive { get; set; }   
       public DateTimeOffset CreatedDate { get; set; }
       public DateTimeOffset UpdatedDate { get; set; }
    }
}
