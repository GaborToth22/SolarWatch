using SolarWatch.Model;

namespace SolarWatch.Service;

public interface IJsonProcessorGeo
{
    Task<City> ProcessAsync(string geoData);
}