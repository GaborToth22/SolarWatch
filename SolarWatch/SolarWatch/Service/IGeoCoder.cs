namespace SolarWatch.Service;

public interface IGeoCoder
{
    Task<string> GeoCodeAsync(string cityName);
}