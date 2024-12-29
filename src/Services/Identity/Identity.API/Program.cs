using BuildingBlock.Installers;
using Identity.API.Data;
using Identity.API.Data.Seeding;
using Identity.API.Implementations;
using Identity.API.Interfaces;
using System.Reflection;

namespace Identity.API
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

            #region Service Register
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddTransient<IDataContextInitializer, DataContextInitializer>();
            #endregion

            #region BuildingBlock
            builder.InstallSerilog();
            builder.Services.InstallSwagger("v1", "JOB ALLEY API Identity");
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
                IDataContextInitializer initializer = services.GetRequiredService<IDataContextInitializer>();
                await initializer.SeedAsync();
                scope.Dispose();
            }

            app.Run();
		}
	}
}
