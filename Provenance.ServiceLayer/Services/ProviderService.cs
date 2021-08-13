using Provenance.Common.Responses;
using Provenance.DataLayer.Base;
using Provenance.DataLayer.Entities;
using Provenance.DataLayer.Repositories;
using Provenance.ServiceLayer.Contracts;
using Provenance.ServiceLayer.DTOs.Provider;
using Provenance.ServiceLayer.Mappings;
using System;

namespace Provenance.ServiceLayer.Services
{
	public class ProviderService : IProviderService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IAccountService _accountService;
		private readonly IProviderRepository _providerRepository;

		public ProviderService (IUnitOfWork unitOfWork,
			IAccountService accountService,
			IProviderRepository providerRepository)
		{
			_unitOfWork = unitOfWork;
			_accountService = accountService;
			_providerRepository = providerRepository;
		}




		public Result Add (AddProviderDTO inputData)
		{
			var canCreate = Provider.CanCreate(_accountService.GetUserId(), inputData.Code, inputData.Name, inputData.Description);
			if (canCreate.Count > 0)
				return Result.Error(canCreate);

			var provider = Provider.Create(_accountService.GetUserId(), inputData.Code, inputData.Name, inputData.Description);

			_providerRepository.Add(provider);

			_unitOfWork.Commit();

			var data = _providerRepository.Get(provider.Id).ToGetProviderDTO();

			return Result.Ok(data, 201, "Object created successfuly");
		}

		public Result Get (Guid id)
		{
			var data = _providerRepository.Get(id).ToGetProviderDTO();
			return Result.Ok(data);
		}

		public Result Update (Guid id, UpdateProviderDTO inputData)
		{

			var provider = _providerRepository.Get(id);

			if (provider == null)
				return Result.Error("there is no provider with that identifier");

			var canUpdate = provider.CanUpdate(inputData.Code, inputData.Name, inputData.Description);
			if (canUpdate.Count > 0)
				return Result.Error(canUpdate);

			provider.Update(inputData.Code, inputData.Name, inputData.Description);

			_providerRepository.Update(provider);

			_unitOfWork.Commit();

			return Result.Ok(null, 200, "Object updated successfuly");

		}

		public Result Delete (Guid id)
		{

			var provider = _providerRepository.Get(id);

			if (provider == null)
				return Result.Error("there is no provider with that identifier");

			_providerRepository.Delete(provider);

			_unitOfWork.Commit();

			return Result.Ok(null, 200, "Object deleted successfuly");

		}

		public Result GetAll ()
		{

			var data = _providerRepository.GetAll().ToGetProviderDTOList();
			return Result.Ok(data);
		}

		public PageableResult GetAll (int page, int pageSize)
		{
			var data = _providerRepository.GetAll(page,pageSize ).ToGetProviderDTOList();
			var count = _providerRepository.GetCount();
			return PageableResult.Ok(data, count, page, pageSize);
		}

		public Result GetByUserId (Guid id)
		{
			var data = _providerRepository.Find(e => e.UserId.Equals(id)).ToGetProviderDTO();
			return Result.Ok(data);
		}

	}
}
