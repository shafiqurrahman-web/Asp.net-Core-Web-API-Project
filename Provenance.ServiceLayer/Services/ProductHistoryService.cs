using Provenance.Common.Responses;
using Provenance.DataLayer.Base;
using Provenance.DataLayer.Entities;
using Provenance.DataLayer.Repositories;
using Provenance.ServiceLayer.Contracts;
using Provenance.ServiceLayer.DTOs.ProductHistory;
using Provenance.ServiceLayer.Mappings;
using System;

namespace Provenance.ServiceLayer.Services
{
	public class ProductHistoryService : IProductHistoryService
	{

		private readonly IUnitOfWork _unitOfWork;
		private readonly IProductHistoryRepository _productHistoryRepository;

		public ProductHistoryService (IUnitOfWork unitOfWork, IProductHistoryRepository productHistoryRepository)
		{
			_unitOfWork = unitOfWork;
			_productHistoryRepository = productHistoryRepository;
		}


		public Result Add (AddProductHistoryDTO inputData)
		{

			var canCreate = ProductHistory.CanCreate(inputData.ProductId, inputData.Date, inputData.Title, inputData.Content, inputData.LocationTitle, inputData.LocationAddress);
			if (canCreate.Count > 0)
				return Result.Error(canCreate);

			var productHistory = ProductHistory.Create(inputData.ProductId, inputData.Date, inputData.Title, inputData.Content, inputData.LocationTitle, inputData.LocationAddress);

			productHistory.SetPicture(inputData.Picture);

			_productHistoryRepository.Add(productHistory);

			_unitOfWork.Commit();

			var data = _productHistoryRepository.GetById(productHistory.Id).ToGetProductHistoryDTO();

			return Result.Ok(data, 201, "Object created successfuly");

		}

		public Result Get (Guid id)
		{
			var data = _productHistoryRepository.GetById(id).ToGetProductHistoryDTO();
			return Result.Ok(data);
		}

		public Result Update (Guid id, UpdateProductHistoryDTO inputData)
		{

			var productHistory = _productHistoryRepository.Get(id);

			if (productHistory == null)
				return Result.Error("there is no productHistory with that identifier");
			
			var canUpdate = ProductHistory.CanUpdate(inputData.ProductId, inputData.Date, inputData.Title, inputData.Content, inputData.LocationTitle, inputData.LocationAddress);
			if (canUpdate.Count > 0)
				return Result.Error(canUpdate);

			productHistory.Update(inputData.ProductId, inputData.Date, inputData.Title, inputData.Content, inputData.LocationTitle, inputData.LocationAddress);

			productHistory.SetPicture(inputData.Picture);

			_productHistoryRepository.Update(productHistory);

			_unitOfWork.Commit();

			return Result.Ok(null, 200, "Object updated successfuly");

		}

		public Result Delete (Guid id)
		{

			var productHistory = _productHistoryRepository.Get(id);

			if (productHistory == null)
				return Result.Error("there is no productHistory with that identifier");

			_productHistoryRepository.Delete(productHistory);

			_unitOfWork.Commit();

			return Result.Ok(null, 200, "Object deleted successfuly");

		}

		public Result GetAll ()
		{
			var data = _productHistoryRepository.GetByCondition(e => true).ToGetProductHistoryDTOList();
			return Result.Ok(data);
		}

		public PageableResult GetAll (int page, int pageSize)
		{
			var data = _productHistoryRepository.GetAll(page, pageSize).ToGetProductHistoryDTOList();
			var count = _productHistoryRepository.GetCount();
			return PageableResult.Ok(data, count, page, pageSize);
		}

		public Result GetProductHistoryByProductId (Guid id)
		{
			var data = _productHistoryRepository.GetProductHistoryByProductId(id).ToGetProductHistoryDTOList();
			return Result.Ok(data);
		}
	}
}
