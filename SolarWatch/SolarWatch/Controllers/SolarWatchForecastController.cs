using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolarWatch.Model;
using SolarWatch.Repository;
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
    private ISolarWatchRepository _solarWatchRepository;

    public SolarWatchForecastController(ILogger<SolarWatchForecastController> logger, IGeoCoder geoCoder, ISunDataProvider sunDataProvider,
        IJsonProcessorSun jsonProcessorSun, IJsonProcessorGeo jsonProcessorGeo, ISolarWatchRepository solarWatchRepository)
    {
        _logger = logger;
        _geoCoder = geoCoder;
        _sunDataProvider = sunDataProvider;
        _jsonProcessorSun = jsonProcessorSun;
        _jsonProcessorGeo = jsonProcessorGeo;
        _solarWatchRepository = solarWatchRepository;
    }

    [HttpGet("SolarWatch"), Authorize]
    public async Task<IActionResult> GetData(string cityName, DateTime date)
    {
        var city = _solarWatchRepository.GetCityByName(cityName);
        if (city == null)
        {
            var geoData = await _geoCoder.GeoCodeAsync(cityName);
            city = await _jsonProcessorGeo.ProcessAsync(geoData);
            _solarWatchRepository.Add(city);
            
        }
        var solarWatchForecast = _solarWatchRepository.GetSolarWatchForecastByCityAndDate(city, date);
        if (solarWatchForecast == null)
        {
            var sunData = await _sunDataProvider.SunDataAsync(city, date);
            solarWatchForecast = await _jsonProcessorSun.ProcessAsync(sunData, city, date);
            _solarWatchRepository.Add(solarWatchForecast);
        }
        try
        {

            return Ok(solarWatchForecast);

        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while processing the data.");
            return StatusCode(500, "Internal Server Error");
        }
    }

}