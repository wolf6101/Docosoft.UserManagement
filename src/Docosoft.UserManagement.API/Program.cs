using System.Reflection;

using Docosoft.UserManagement.Application;
using Docosoft.UserManagement.Infrastructure;

using MediatR;

public partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
        builder.Services.ConfigureInfrastructureServices(builder.Configuration);
        builder.Services.ConfigureApplicationServices(builder.Configuration);

        builder.Host.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddConsole();
        });

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}