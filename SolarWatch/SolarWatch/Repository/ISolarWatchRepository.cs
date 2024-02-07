using SolarWatch.Model;

namespace SolarWatch.Repository;

public interface ISolarWatchRepository
{
    IEnumerable<City> GetAllCities();
    City? GetCityByName(string name);

    void Add(City city);
    void Delete(City city);
    void Update(City city);
    
    IEnumerable<SolarWatchForecast> GetAllSolarWatchForecast();
    SolarWatchForecast? GetSolarWatchForecastByCityAndDate(City city, DateTime dateTime);

    void Add(SolarWatchForecast solarWatchForecast);
    void Delete(SolarWatchForecast solarWatchForecast);
    void Update(SolarWatchForecast solarWatchForecast);
}