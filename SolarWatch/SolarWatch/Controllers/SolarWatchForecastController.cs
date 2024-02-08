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

    [HttpGet("SolarWatch"), Authorize(Roles="User, Admin")]
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

    [HttpPost("PostCity"), Authorize(Roles="Admin")]
    public async Task<IActionResult> PostCity(string name, string country, string state, double lat, double lon)
    {
        var city = new City
        {
            Name = name,
            Country = country,
            State = state,
            Lat = lat,
            Lon = lon
        };
        _solarWatchRepository.Add(city);
        try
        {
            return Ok(city);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while processing the data.");
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpDelete("DeleteCity"), Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteCityById(int id)
    {
        var city = _solarWatchRepository.GetCityById(id);
        if (city != null)
        {
            _solarWatchRepository.Delete(city);
            try
            {
                return Ok($"city with Id: {id} deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing the data.");
                return StatusCode(500, "Internal Server Error");
            }
        }

        return NotFound($"City with Id: {id} not found");
    }

    [HttpPatch("UpdateCity"), Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateCity(int id, string? name, string? country, string? state, double? lat, double? lon)
    {
        var city = _solarWatchRepository.GetCityById(id);
        if (city == null)
        {
            return NotFound($"City with Id: {id} not found");
        }
        
        if (name != null)
        {
            city.Name = name;
        }
        if (country != null)
        {
            city.Country = country;
        }
        if (state != null)
        {
            city.State = state;
        }
        if (lat != null)
        {
            city.Lat = lat.Value;
        }
        if (lon != null)
        {
            city.Lon = lon.Value;
        }
        
        _solarWatchRepository.Update(city);

        try
        {
            return Ok(city);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while processing the data.");
            return StatusCode(500, "Internal Server Error");
        }
    }
    
    [HttpDelete("DeleteSolarWatchForecast"), Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteSolarForecastById(int id)
    {
        var solarWatchForecast = _solarWatchRepository.GetSolarWatchForecastById(id);
        if (solarWatchForecast != null)
        {
            _solarWatchRepository.Delete(solarWatchForecast);
            try
            {
                return Ok($"solarWatchForecast with Id: {id} deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing the data.");
                return StatusCode(500, "Internal Server Error");
            }
        }

        return NotFound($"solarWatchForecast with Id: {id} not found");
    }
    
    [HttpPost("PostSolarWatchForecast"), Authorize(Roles="Admin")]
    public async Task<IActionResult> PostSolarWatchForecast(DateTime date, string sunset, string sunrise, int cityId)
    {
        var city = _solarWatchRepository.GetCityById(cityId);
        if (city == null)
        {
            return NotFound($"City with Id: {cityId} not found");
        }
        var solarWatchForecast = new SolarWatchForecast
        {
            Date = date,
            SunSet = sunset,
            SunRise = sunrise,
            CityId = cityId
        };
        _solarWatchRepository.Add(solarWatchForecast);
        
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
    
    [HttpPatch("UpdateSolarWatchForecast"), Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateSolarWatchForecast(int id, DateTime? date, string? sunRise, string? sunSet, int? cityId)
    {
        var solarWatchForecast = _solarWatchRepository.GetSolarWatchForecastById(id);
        if (solarWatchForecast == null)
        {
            return NotFound($"SolarWatchForecast with Id: {id} not found");
        }

        if (cityId != null)
        {
            var city = _solarWatchRepository.GetCityById((int)cityId);
            if (city == null)
            {
                return NotFound($"City with Id: {cityId} not found");
            }

            solarWatchForecast.CityId = (int)cityId;
        }
        
        if (sunRise != null)
        {
            solarWatchForecast.SunRise = sunRise;
        }
        if (sunSet != null)
        {
            solarWatchForecast.SunSet = sunSet;
        }
        if (date != null)
        {
            solarWatchForecast.Date = (DateTime)date;
        }
        
        
        _solarWatchRepository.Update(solarWatchForecast);

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