using HttpClientApi.Services.Interfaces;

namespace HttpClientApi.Services.Dependencias
{
	//Para facilitar o entendimento deste projeto, dediquei esta classe para configurar os meus serviços de API.
	//Dessa forma, o arquivo Program.cs não ficará imenso.
	public static class ServicesConfig
	{
		public static void AddHttpServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddHttpClient<IPostService, PostService>(httpClient =>
			{
				httpClient.BaseAddress = GetConnection(builder);
			});
			builder.Services.AddHttpClient<ICommentService, CommentService>(httpClient =>
			{
				httpClient.BaseAddress = GetConnection(builder);
			});
		}
		private static Uri GetConnection(WebApplicationBuilder builder)
		{
			return new Uri(builder.Configuration["APICONNECTION"]!);
		}
	}
}
