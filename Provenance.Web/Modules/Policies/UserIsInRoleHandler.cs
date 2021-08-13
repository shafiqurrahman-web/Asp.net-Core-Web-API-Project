using Microsoft.AspNetCore.Authorization;
using Provenance.Common.Configurations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Provenance.Web.Modules.Policies
{
	public class UserIsInRoleHandler : AuthorizationHandler<UserIsInRoleRequirement>
	{
		protected override Task HandleRequirementAsync (AuthorizationHandlerContext context, UserIsInRoleRequirement requirement)
		{
			if (!context.User.HasClaim(c => c.Type == ClaimTypes.Role) ||
				!context.User.HasClaim(c => c.Type.ToLower() == CustomClaims.ROLE))
			{
				var roles = context.User.Claims.Where(e => e.Type.ToLower() == CustomClaims.ROLE || e.Type == ClaimTypes.Role)
					.Select(e => e.Value.ToLower())
					.ToList();

				if (roles.Contains(requirement.Role) || requirement.Role == RoleTypes.ADMIN)
				{
					context.Succeed(requirement);
				}
				else
				{
					context.Fail();
				}
			}
			return Task.CompletedTask;
		}


	}
}
