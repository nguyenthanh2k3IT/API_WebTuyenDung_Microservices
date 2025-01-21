
using BuildingBlock.Installers;
using Job.Application;
using Job.Application.Services;
using Job.Infrastructure;
using Job.Infrastructure.Data;
using System.Reflection;

namespace Job.API
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region DependencyInjection
            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddScoped<CVAnalyzerService>();
            #endregion

            #region BuildingBlock
            builder.InstallSerilog();
            builder.Services.InstallSwagger("v1", "JOBALLEY.API - JOB SERVICE");
            builder.Services.InstallCORS();
            builder.Services.InstallMediatR(Assembly.GetExecutingAssembly());
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            builder.Services.InstallAuthentication();
            #endregion

            var app = builder.Build();
            app.UseSwaggerService();
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionMiddleware>();
            app.MigrationAutoUpdate<DataContext>();
            app.MapControllers();
            await app.SeedingData();

            app.Run();
        }
    }
}
