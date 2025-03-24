
using HttpClientApi.Services.Dependencias;
using Scalar.AspNetCore;

namespace HttpClientApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

			builder.Services.AddAutoMapper(typeof(Program));
			builder.AddHttpServices();

			var app = builder.Build();
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.MapOpenApi();
				app.MapScalarApiReference(opt =>
				{
					opt.Title = "Testes HttpClient";
					opt.Theme = ScalarTheme.BluePlanet;
					opt.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
				});
			}
			app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
