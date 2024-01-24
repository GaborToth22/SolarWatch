using SolarWatch.Model;

namespace SolarWatch.Service;

public interface IJsonProcessorSun
{
    SolarWatchForecast Process(string sunData, City city, DateTime date);
}