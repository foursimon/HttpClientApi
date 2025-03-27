using Microsoft.AspNetCore.Authentication.Certificate;
using HttpClientApi.Services.Dependencias;
using Scalar.AspNetCore;

namespace HttpClientApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
			//nome da minha politica
			var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
			var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
			//Politica CORS para aceitar requisições de outros pontos de origem
			builder.Services.AddCors(opt =>
			{
				opt.AddPolicy(name: MyAllowSpecificOrigins, policy =>
				{
					//Especificando quais origens serão aceitas
					policy.WithOrigins("http://localhost:5173");
					//Especificando quais headers dessas origens serão aceitos
					policy.WithHeaders("content-type", "api-key");
				});
			});
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
					opt.WithDefaultHttpClient(ScalarTarget.Node, ScalarClient.Fetch);
				});
			}
			app.UseHttpsRedirection();

            app.UseAuthorization();
			app.UseAuthentication();
			//Usando a politica Cors
			app.UseCors(MyAllowSpecificOrigins);

            app.MapControllers();

            app.Run();
        }
    }
}
