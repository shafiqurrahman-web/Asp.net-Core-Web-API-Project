using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IAuthorizationService = Provenance.ServiceLayer.Contracts.IAuthorizationService;

namespace Provenance.Web.Modules.Policies
{
	public class UserIsActiveHandler : AuthorizationHandler<UserIsActiveRequirement>
	{
		private readonly IAuthorizationService authService;

		public UserIsActiveHandler (IAuthorizationService authService)
		{
			this.authService = authService;
		}

		protected override Task HandleRequirementAsync (AuthorizationHandlerContext context, UserIsActiveRequirement requirement)
		{
			if (!context.User.HasClaim(c => c.Type == "email" || c.Type == ClaimTypes.Email || c.Type == "emailaddress"))
			{
				//TODO: Use the following if targeting a version of
				//.NET Framework older than 4.6:
				//      return Task.FromResult(0);
				return Task.CompletedTask;
			}


			var userIsActive = authService.IsActive(
				context.User.Claims
				.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email) || c.Type.Equals("email"))
				.Value
			);


			if (userIsActive)
				context.Succeed(requirement);

			//TODO: Use the following if targeting a version of
			//.NET Framework older than 4.6:
			//      return Task.FromResult(0);
			return Task.CompletedTask;
		}
	}
}
