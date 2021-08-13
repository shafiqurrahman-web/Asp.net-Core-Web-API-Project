using Provenance.Common.Responses;
using Provenance.ServiceLayer.DTOs.Provider;
using System;

namespace Provenance.ServiceLayer.Contracts
{
	public interface IProviderService
	{
		Result Add (AddProviderDTO inputData);

		Result Get (Guid id);

		Result Update (Guid id, UpdateProviderDTO inputData);

		Result Delete (Guid id);

		Result GetAll ();

		PageableResult GetAll (int page, int pageSize);

		Result GetByUserId (Guid id);

	}
}
