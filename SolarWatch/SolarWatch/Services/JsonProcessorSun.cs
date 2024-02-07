using System.Text.Json;
using SolarWatch.Model;

namespace SolarWatch.Service;

public class JsonProcessorSun : IJsonProcessorSun
{
   
    public async Task<SolarWatchForecast> ProcessAsync(string sunData, City city, DateTime date)
    {
        JsonDocument sunJson = JsonDocument.Parse(sunData);
        JsonElement sunRise = sunJson.RootElement.GetProperty("results").GetProperty("sunrise");
        JsonElement sunSet = sunJson.RootElement.GetProperty("results").GetProperty("sunset");
        
        
        SolarWatchForecast forecast = new SolarWatchForecast
        {
            SunRise = sunRise.ToString(),
            SunSet = sunSet.ToString(),
            CityId = city.Id,
            Date = date,
        };

        return forecast;
    }
}