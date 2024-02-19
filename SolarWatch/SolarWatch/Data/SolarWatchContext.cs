using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using SolarWatch.Model;

namespace SolarWatch.Data;

public class SolarWatchContext : DbContext
{
    public DbSet<City> Cities { get; set; }
    public DbSet<SolarWatchForecast> SolarWatchForecasts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dotenv = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", ".env");
        Env.Load(dotenv);
        optionsBuilder.UseSqlServer(
            $"Server={Environment.GetEnvironmentVariable("DBHOST")},{Environment.GetEnvironmentVariable("DBPORT")};" +
            $"Database={Environment.GetEnvironmentVariable("DBNAME")};User Id={Environment.GetEnvironmentVariable("DBUSER")};" +
            $"Password={Environment.GetEnvironmentVariable("DBPASSWORD")};Encrypt=false;TrustServerCertificate=true;"); 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<City>()
            .HasMany(c => c.SolarWatchForecasts)
            .WithOne(s => s.City)
            .HasForeignKey(s => s.CityId)
            .IsRequired();
    }
}
