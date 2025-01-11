
using BuildingBlock.Installers;
using System.Reflection;

namespace Job.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region BuildingBlock
            builder.InstallSerilog();
            builder.Services.InstallSwagger("v1", "JOBALLEY.API - JOB SERVICE");
            builder.Services.InstallCORS();
            builder.Services.InstallMediatR(Assembly.GetExecutingAssembly());

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
           // app.MigrationAutoUpdate<DataContext>();
            app.MapControllers();

            app.Run();
        }
    }
}
