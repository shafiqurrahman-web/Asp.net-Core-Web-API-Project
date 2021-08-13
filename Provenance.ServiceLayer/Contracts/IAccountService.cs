using Provenance.Common.Responses;
using Provenance.ServiceLayer.DTOs.Account;
using System;

namespace Provenance.ServiceLayer.Contracts
{
	public interface IAccountService
	{
		Guid GetUserId ();
		void SetUserId (Guid id);
		Result GetUserById (Guid userId);
		Result AddUserFromToken (AddUserFromTokenDTO data);
	}
}
