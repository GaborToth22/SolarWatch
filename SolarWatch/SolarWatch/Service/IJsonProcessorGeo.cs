using SolarWatch.Model;

namespace SolarWatch.Service;

public interface IJsonProcessorGeo
{
    City Process(string geoData);
}