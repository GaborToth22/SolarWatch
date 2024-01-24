using SolarWatch.Model;

namespace SolarWatch.Service;

public interface ISunDataProvider
{
    Task<string> SunDataAsync(City city, DateTime date);
}