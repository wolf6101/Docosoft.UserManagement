using System.Reflection;

using Docosoft.UserManagement.Application.SeedWork;
using Docosoft.UserManagement.Application.Users;

using MediatR;
using MediatR.Pipeline;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Docosoft.UserManagement.Application
{
    public static class Dependencies
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestExceptionProcessorBehavior<,>));
            services.AddScoped(typeof(IRequestExceptionHandler<CreateUserCommand, ResponseDto<UserDto>, Exception>), typeof(CreateUserCommandExceptionHandler));
            
            return services;
        }
    }
}