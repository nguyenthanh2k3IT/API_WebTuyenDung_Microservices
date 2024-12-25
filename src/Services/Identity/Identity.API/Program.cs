using BuildingBlock.Installers;
using Identity.API.Data;
using System.Reflection;

namespace Identity.API
{
	public class Program
	{
		public static void Main(string[] args)
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

            #region BuildingBlock
            builder.InstallSerilog();
            builder.Services.InstallSwagger("v1", "JOB ALLEY API Identity");
            builder.Services.InstallCORS();
            builder.Services.InstallAuthentication();
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

            app.Run();
		}
	}
}
