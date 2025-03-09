using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WorkPulse.Persistence.DataContext;

using Microsoft.Extensions.Configuration;

public class EfDataContextFactory : IDesignTimeDbContextFactory<EfDataContext>
{
    public EfDataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EfDataContext>();

        
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "DataContext") ) 
            .AddJsonFile("appsettings.json" , optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
        Console.WriteLine(configuration.GetConnectionString("DefaultConnection"));
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        return new EfDataContext(optionsBuilder.Options, configuration);
    }
}
