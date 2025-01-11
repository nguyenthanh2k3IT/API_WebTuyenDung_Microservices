using BuildingBlock.Installers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Job.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddAutoMapper(assembly);
        services.InstallMediatR(assembly);
        return services;
    }

    /*public static WebApplication UseGrpcRouting(this WebApplication app)
    {
        app.MapGrpcService<CatalogService>();
        return app;
    }*/
}