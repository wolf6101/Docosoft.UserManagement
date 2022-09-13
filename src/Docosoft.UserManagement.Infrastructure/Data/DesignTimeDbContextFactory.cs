using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Docosoft.UserManagement.Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<UserContext>
    {
        // This class' single purpose is to let dbcontext registration and migrations out of startup project
        // This design decision is made to avoid coupling between API and specific ORM (EF Core)
        public UserContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<UserContext>();
            var connectionString = configuration.GetConnectionString("Main");
            builder.UseSqlServer(connectionString);

            return new UserContext(builder.Options);
        }
    }
}