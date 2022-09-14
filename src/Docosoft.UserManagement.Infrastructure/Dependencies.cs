using Docosoft.UserManagement.Domain.Users.Repositories;
using Docosoft.UserManagement.Infrastructure.Data;
using Docosoft.UserManagement.Infrastructure.Domain.Users.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Docosoft.UserManagement.Infrastructure
{
    public static class Dependencies
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var useInMemory = bool.Parse(configuration["UseInMemoryDatabase"] ?? "false");

            if (useInMemory)
            {
                ConfigureInMemoryDatabase(services);
            }
            else
            {
                ConfigureDatabase(services, configuration);
            }

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();

            return services;
        }

        public static void ApplyMigrations(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            using (var context = serviceProvider.GetService<UserContext>())
            {
                context.Database.Migrate();
            }
        }

        public static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Main");

            services.AddDbContext<UserContext>(options => options.UseSqlServer(connectionString));
            ApplyMigrations(services);
        }

        public static void ConfigureInMemoryDatabase(IServiceCollection services)
        {
            services.AddDbContext<UserContext>(options => options.UseInMemoryDatabase("InMemoryUserManagement"));

            var serviceProvider = services.BuildServiceProvider();

            using (var context = serviceProvider.GetService<UserContext>())
            {
                context.Database.EnsureCreated(); // Seeds InMemory database
            }
        }
    }
}