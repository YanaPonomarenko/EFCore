using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Academy.Context
{
    public class AcademyDbContextFactory
     : IDesignTimeDbContextFactory<AcademyDbContext>
    {
        public AcademyDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AcademyDbContext>();

            DbContextConfigurator.Configure(optionsBuilder);

            return new AcademyDbContext(optionsBuilder.Options);
        }
    }
}
