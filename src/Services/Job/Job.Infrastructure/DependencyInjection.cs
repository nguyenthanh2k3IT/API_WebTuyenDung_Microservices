using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Job.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();
        var cnStr = configuration.GetConnectionString("Database");
        Console.WriteLine(cnStr);
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddDbContext<DataContext>(options => options.UseNpgsql(cnStr));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddSingleton(TimeProvider.System);
        services.AddScoped<IDataContext>(provider => provider.GetRequiredService<DataContext>());
        services.AddScoped<IDataInitializer, DataInitializer>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

    public async static Task<WebApplication> SeedingData(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            IServiceProvider services = scope.ServiceProvider;
            IDataInitializer initializer = services.GetRequiredService<IDataInitializer>();
            await initializer.SeedAsync();
            scope.Dispose();
        }

        return app;
    }
}