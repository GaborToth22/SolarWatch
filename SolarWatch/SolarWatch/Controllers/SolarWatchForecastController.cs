using Microsoft.AspNetCore.Mvc;
using SolarWatch.Model;
using SolarWatch.Service;

namespace SolarWatch.Controllers;

[ApiController]
[Route("[controller]")]

public class SolarWatchForecastController : ControllerBase
{
    private readonly ILogger<SolarWatchForecastController> _logger;
    private IGeoCoder _geoCoder;
    private ISunDataProvider _sunDataProvider;
    private IJsonProcessorSun _jsonProcessorSun;
    private IJsonProcessorGeo _jsonProcessorGeo;

    public SolarWatchForecastController(ILogger<SolarWatchForecastController> logger, IGeoCoder geoCoder, ISunDataProvider sunDataProvider, IJsonProcessorSun jsonProcessorSun, IJsonProcessorGeo jsonProcessorGeo)
    {
        _logger = logger;
        _geoCoder = geoCoder;
        _sunDataProvider = sunDataProvider;
        _jsonProcessorSun = jsonProcessorSun;
        _jsonProcessorGeo = jsonProcessorGeo;
    }

    [HttpGet("SolarWatch")]
    public async Task<IActionResult> GetData(string cityName, DateTime date)
    {
        try
        {
            var geoData = await _geoCoder.GeoCodeAsync(cityName);
            var city = await _jsonProcessorGeo.ProcessAsync(geoData);
            var sunData = await _sunDataProvider.SunDataAsync(city, date);

            return Ok(_jsonProcessorSun.Process(sunData, city, date));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while processing the data.");
            return StatusCode(500, "Internal Server Error");
        }
    }

}