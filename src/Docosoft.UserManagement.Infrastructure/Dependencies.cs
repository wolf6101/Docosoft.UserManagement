using Docosoft.UserManagement.Domain.Users;
using Docosoft.UserManagement.Infrastructure.Domain.Users;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Docosoft.UserManagement.Infrastructure
{
    public static class Dependencies
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Main");
            services.AddDbContext<UserContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();

            ApplyMigrations(services);

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
    }
}