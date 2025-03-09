using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WorkPulse.Persistence.Users;

namespace WorkPulse.Persistence.DataContext;

public class EfDataContext : DbContext
{
    private readonly IConfiguration _configuration;
    
    public EfDataContext(DbContextOptions<EfDataContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEntityMap).Assembly);
    }
}
