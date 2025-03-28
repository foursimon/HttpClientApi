using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace HttpClientApi
{
	//Classe atributo responsável por garantir se a requisição feita para essa API
	//possui a chave correta.
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	internal sealed class ApiKeyAttribute : Attribute, IAuthorizationFilter
	{
		//Definindo o nome do cabeçalho da Api
		private const string ApiKeyHeaderName = "Api-Key";
		//Método da interface IAuthotizaFilter que verifica, através do context HTTP, se a requisição
		//possui autorização ou não.
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			//Retorna o código 401 (não autorizado) caso a chave enviada não for válida
			if (!ChaveValida(context.HttpContext))
			{
				context.Result = new UnauthorizedResult();
			}
		}
		private static bool ChaveValida(HttpContext httpContext)
		{
			//Pega a chave enviada no cabeçalho da requisição
			string? apiKey = httpContext.Request.Headers[ApiKeyHeaderName];
			//retorna falso se for nula ou espaço em branco
			if (string.IsNullOrWhiteSpace(apiKey))
			{
				return false;
			}
			//Pega a chave dessa API armazenada nos segredos do usuário
			string actualApiKey = httpContext.RequestServices
				.GetRequiredService<IConfiguration>()
				.GetValue<string>("APIKEY")!;
			//verifica se a chave da requisição é igual a chave armazenada nessa API
			return apiKey == actualApiKey;
		}
	}
}
