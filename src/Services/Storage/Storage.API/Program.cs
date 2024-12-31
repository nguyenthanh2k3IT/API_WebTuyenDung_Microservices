
using BuildingBlock.Installers;
using System.Reflection;

namespace Storage.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpClient();

            #region BuildingBlock
            builder.InstallSerilog();
            builder.Services.InstallSwagger("v1", "JOB ALLEY API - STORAGE SERVICE");
            builder.Services.InstallCORS();
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
            app.MapControllers();

            app.Run();
        }
    }
}
