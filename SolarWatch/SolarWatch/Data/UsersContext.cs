using DotNetEnv;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SolarWatch.Data;

public class UsersContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public UsersContext(DbContextOptions<UsersContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var dotenv = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", ".env");
        Env.Load(dotenv);
        options.UseSqlServer($"Server={Environment.GetEnvironmentVariable("DBHOST")},{Environment.GetEnvironmentVariable("DBPORT")};" +
                             $"Database={Environment.GetEnvironmentVariable("DBNAME")};User Id={Environment.GetEnvironmentVariable("DBUSER")};" +
                             $"Password={Environment.GetEnvironmentVariable("DBPASSWORD")};Encrypt=false;TrustServerCertificate=true;"); 
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}