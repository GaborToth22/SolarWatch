using System.ComponentModel.DataAnnotations.Schema;

namespace SolarWatch.Model;

public class SolarWatchForecast
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string SunRise { get; set; }
    public string SunSet { get; set; }
    public int CityId { get; set; }
    public City City { get; set; }
}