using System.Net;

namespace SolarWatch.Service;

public class GeoCoder : IGeoCoder
{
    private readonly ILogger<GeoCoder> _logger;
    
    public GeoCoder(ILogger<GeoCoder> logger)
    {
        _logger = logger;
    }
    
    public string GeoCode(string city)
    {
        var apiKey = "ad422c08f3020b8fee82757d8a0cacd8";
        var url = $"http://api.openweathermap.org/geo/1.0/direct?q={city}&limit=5&appid={apiKey}";
        
        var client = new WebClient();
        
        _logger.LogInformation("Calling OpenWeather Api with url: {url}", url);
        return client.DownloadString(url);
    }
}