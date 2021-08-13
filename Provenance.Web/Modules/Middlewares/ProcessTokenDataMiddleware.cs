using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Provenance.ServiceLayer.Contracts;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Provenance.Web.Modules.Middlewares
{
	public class ProcessTokenDataMiddleware
	{

		private readonly RequestDelegate _next;

		public ProcessTokenDataMiddleware (RequestDelegate next)
		{
			_next = next;
		}

		// IMyScopedService is injected into Invoke
		public async Task Invoke (HttpContext httpContext, IAccountService accountService)
		{
			if (httpContext.User != null && httpContext.User.Identity != null && httpContext.User.Identity.IsAuthenticated)
			{
				var userClaims = (ClaimsIdentity)httpContext.User.Identity;
				Guid.TryParse(userClaims.FindFirst(e => e.Type.Equals("sub")).Value, out Guid userId);
				accountService.SetUserId(userId);
			}
			await _next(httpContext);
		}
	}

	public static class ProcessTokenDataMiddlewareExtension
	{
		public static IApplicationBuilder UseProcessTokenDataMiddleware (this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<ProcessTokenDataMiddleware>();
		}
	}

}
