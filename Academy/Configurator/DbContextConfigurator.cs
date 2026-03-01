using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public static class DbContextConfigurator
{
    public static void Configure(DbContextOptionsBuilder options)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        options.UseSqlServer(
            configuration.GetConnectionString("MSSQLConnection"));
    }
}