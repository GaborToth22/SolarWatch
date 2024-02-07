namespace SolarWatch.Model;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
    public List<SolarWatchForecast> SolarWatchForecasts { get; set; }
}