using System.Net;
using SolarWatch.Model;

namespace SolarWatch.Service;

public class SunDataProvider : ISunDataProvider
{
    private readonly ILogger<SunDataProvider> _logger;
    
    public SunDataProvider(ILogger<SunDataProvider> logger)
    {
        _logger = logger;
    }
    public async Task<string> SunDataAsync(City city, DateTime date)
    {
        var url = $"https://api.sunrise-sunset.org/json?lat={city.Lat}&lng={city.Lon}&date={date}";

        var client = new HttpClient();
        
        _logger.LogInformation("Calling Sunrise Sunset Api with url: {url}", url);
        return await client.GetStringAsync(url);
    }
}