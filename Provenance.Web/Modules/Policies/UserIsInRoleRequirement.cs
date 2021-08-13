using Microsoft.AspNetCore.Authorization;

namespace Provenance.Web.Modules.Policies
{
	public class UserIsInRoleRequirement : IAuthorizationRequirement
	{
		public string Role;

		public UserIsInRoleRequirement (string role)
		{
			Role = role;
		}

	}
}
