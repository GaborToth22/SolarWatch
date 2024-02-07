using SolarWatch.Model;

namespace SolarWatch.Service;

public interface IJsonProcessorSun
{
    Task<SolarWatchForecast> ProcessAsync(string sunData, City city, DateTime date);
}