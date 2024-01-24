using System.Text.Json;
using SolarWatch.Model;

namespace SolarWatch.Service;

public class JsonProcessorGeo : IJsonProcessorGeo
{
    public async Task<City> ProcessAsync(string geoData)
    {
        JsonDocument geoJson = JsonDocument.Parse(geoData);
        if (geoJson.RootElement.EnumerateArray().Any())
        {
            JsonElement cityName = geoJson.RootElement[0].GetProperty("name");
            JsonElement lat = geoJson.RootElement[0].GetProperty("lat");
            JsonElement lon = geoJson.RootElement[0].GetProperty("lon");

            City city = new City
            {
                Name = cityName.ToString(),
                Lat = lat.GetDouble(),
                Lon = lon.GetDouble()
            };
            return city;
        }
        else
        {
            City city = new City
            {
                Name = "Invalid City name",
                Lat = 0,
                Lon = 0
            };
            return city;
        }
    }
}