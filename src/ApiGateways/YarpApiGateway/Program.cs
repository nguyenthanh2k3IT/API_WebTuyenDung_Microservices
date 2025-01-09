namespace YarpApiGateway;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    /*builder.AllowAnyOrigin()
							.AllowAnyMethod()
							.AllowAnyHeader()
							.WithExposedHeaders("Content-Disposition");*/

                    builder.WithOrigins("http://localhost:3003")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials()
                       .WithExposedHeaders("Content-Disposition");
                });
        });

        builder.Services.AddReverseProxy()
			.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

		var app = builder.Build();
		app.MapReverseProxy();
		app.Run();
	}
}
