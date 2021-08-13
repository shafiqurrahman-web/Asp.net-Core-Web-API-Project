using System;

namespace Provenance.ServiceLayer.Contracts
{
	public interface IAuthorizationService
	{
		bool IsActive (string email);
		bool IsActive (Guid id);

	}
}
