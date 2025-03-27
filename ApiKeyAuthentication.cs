using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace HttpClientApi
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	internal sealed class ApiKeyAttribute : Attribute, IAuthorizationFilter
	{
		private const string ApiKeyHeaderName = "Api-Key";
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			if (!ChaveValida(context.HttpContext))
			{
				context.Result = new UnauthorizedResult();
			}
		}

		private static bool ChaveValida(HttpContext httpContext)
		{
			string? apiKey = httpContext.Request.Headers[ApiKeyHeaderName];
			if (string.IsNullOrWhiteSpace(apiKey))
			{
				return false;
			}
			string actualApiKey = httpContext.RequestServices
				.GetRequiredService<IConfiguration>()
				.GetValue<string>("APIKEY")!;
			return apiKey == actualApiKey;
		}
	}
}
