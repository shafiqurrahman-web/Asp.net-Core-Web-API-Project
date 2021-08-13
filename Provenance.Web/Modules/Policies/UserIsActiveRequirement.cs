using Microsoft.AspNetCore.Authorization;

namespace Provenance.Web.Modules.Policies
{
	public class UserIsActiveRequirement : IAuthorizationRequirement
	{

		public UserIsActiveRequirement ()
		{
		}

	}
}
