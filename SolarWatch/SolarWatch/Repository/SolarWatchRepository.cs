using Microsoft.EntityFrameworkCore;
using SolarWatch.Data;
using SolarWatch.Model;

namespace SolarWatch.Repository;

public class SolarWatchRepository : ISolarWatchRepository
{
    
    public IEnumerable<City> GetAllCities()
    {
        using var dbContext = new SolarWatchContext();
        return dbContext.Cities.ToList();
    }

    public City? GetCityByName(string name)
    {
        using var dbContext = new SolarWatchContext();
        return dbContext.Cities.FirstOrDefault(c => c.Name == name);
    }
    
    public City? GetCityById(int id)
    {
        using var dbContext = new SolarWatchContext();
        return dbContext.Cities.FirstOrDefault(c => c.Id == id);
    }

    public void Add(City city)
    {
        using var dbContext = new SolarWatchContext();
        dbContext.Cities.Add(city);
        dbContext.SaveChanges();
    }

    public void Delete(City city)
    {
        using var dbContext = new SolarWatchContext();
        dbContext.Cities.Remove(city);
        dbContext.SaveChanges();
    }

    public void Update(City city)
    {
        using var dbContext = new SolarWatchContext();
        dbContext.Cities.Update(city);
        dbContext.SaveChanges();
    }

    public IEnumerable<SolarWatchForecast> GetAllSolarWatchForecast()
    {
        using var dbContext = new SolarWatchContext();
        return dbContext.SolarWatchForecasts.ToList();
    }

    public SolarWatchForecast? GetSolarWatchForecastByCityAndDate(City city, DateTime dateTime)
    {
        using var dbContext = new SolarWatchContext();
        return dbContext.SolarWatchForecasts.Include(s=>s.City).FirstOrDefault(s => s.City == city && s.Date == dateTime);
    }

    public SolarWatchForecast? GetSolarWatchForecastById(int id)
    {
        using var dbContext = new SolarWatchContext();
        return dbContext.SolarWatchForecasts.FirstOrDefault(s => s.Id == id);
    }

    public void Add(SolarWatchForecast solarWatchForecast)
    {
        using var dbContext = new SolarWatchContext();
        dbContext.SolarWatchForecasts.Add(solarWatchForecast);
        dbContext.SaveChanges();
    }

    public void Delete(SolarWatchForecast solarWatchForecast)
    {
        using var dbContext = new SolarWatchContext();
        dbContext.SolarWatchForecasts.Remove(solarWatchForecast);
        dbContext.SaveChanges();
    }

    public void Update(SolarWatchForecast solarWatchForecast)
    {
        using var dbContext = new SolarWatchContext();
        dbContext.SolarWatchForecasts.Update(solarWatchForecast);
        dbContext.SaveChanges();
    }
}