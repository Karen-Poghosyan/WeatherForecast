using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Core.Enums;

namespace WeatherForecast.Core.Helpers
{
    public static class WeatherHelper
    {
        public static WeatherCondition CalculateAverageCondition(List<WeatherCondition> conditions)
        {
            if (conditions == null || !conditions.Any())
                return WeatherCondition.Unknown;

            Dictionary<WeatherCondition, int> conditionWeights = new Dictionary<WeatherCondition, int>
        {
            { WeatherCondition.Clear, 1 },
            { WeatherCondition.MostlyClear, 1 },
            { WeatherCondition.PartlyCloudy, 2 },
            { WeatherCondition.MostlyCloudy, 3 },
            { WeatherCondition.Cloudy, 4 },
            { WeatherCondition.LightRain, 5 },
            { WeatherCondition.ModerateRain, 6 },
            { WeatherCondition.HeavyRain, 7 },
            { WeatherCondition.LightSnow, 8 },
            { WeatherCondition.ModerateSnow, 9 },
            { WeatherCondition.HeavySnow, 10 },
            { WeatherCondition.Thunderstorm, 11 },
            { WeatherCondition.Mist, 12 },
            { WeatherCondition.Fog, 13 },
            { WeatherCondition.Hail, 14 },
            { WeatherCondition.Freezing, 15 },
            { WeatherCondition.Unknown, 16 }
        }; // adding weights for other conditions

            int totalWeight = 0;
            foreach (var condition in conditions)
            {
                if (conditionWeights.TryGetValue(condition, out int weight))
                    totalWeight += weight;
            }

            // calculate the weighted average condition
            WeatherCondition averageCondition = conditionWeights
                .OrderByDescending(kv => kv.Value)
                .FirstOrDefault(kv => totalWeight >= kv.Value)
                .Key;

            return averageCondition;
        }
    }

}
