using Blog.API.Data;
using Blog.API.Data.Seeding;
using BuildingBlock.Installers;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Blog.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var environment = builder.Configuration.GetSection("HostSettings")["Environment"];

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IDataInitializer, DataInitializer>();

            #region BuildingBlock
            builder.InstallSerilog();
            builder.Services.InstallSwagger("v1", "JOB_ALLEY.API BLOG");
            builder.Services.InstallCORS();
            builder.Services.InstallAuthentication();
            builder.Services.InstallMediatR(Assembly.GetExecutingAssembly());
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #endregion

            #region DataContext
            var cnStr = builder.Configuration.GetConnectionString("Database");
            builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(cnStr));
            #endregion

            var app = builder.Build();

            app.UseSwaggerService();
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionMiddleware>();
            app.MapControllers();
            app.MigrationAutoUpdate<DataContext>();

            using (var scope = app.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                IDataInitializer initializer = services.GetRequiredService<IDataInitializer>();
                await initializer.SeedAsync();
                scope.Dispose();
            }

            app.Run();
        }
    }
}
