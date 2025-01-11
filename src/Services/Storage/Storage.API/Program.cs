using BuildingBlock.Installers;
using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using Storage.API.Configurations;
using Storage.API.Interfaces;
using Storage.API.Services;

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

            var cloudinaryConfiguration = builder.Configuration.GetSection("CloudinarySettings");
            builder.Services.Configure<CloudinarySettings>(cloudinaryConfiguration);

            builder.Services.AddSingleton(provider =>
            {
                var config = provider.GetRequiredService<IOptions<CloudinarySettings>>().Value;
                return new Cloudinary(new Account(config.CloudName, config.ApiKey, config.ApiSecret));
            });
            builder.Services.AddTransient<IStorageService, StorageService>();

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
