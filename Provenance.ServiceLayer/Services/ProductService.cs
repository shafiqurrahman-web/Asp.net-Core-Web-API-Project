using Provenance.Common.Responses;
using Provenance.DataLayer.Base;
using Provenance.DataLayer.Entities;
using Provenance.DataLayer.Repositories;
using Provenance.ServiceLayer.Contracts;
using Provenance.ServiceLayer.DTOs.Product;
using Provenance.ServiceLayer.Mappings;
using System;

namespace Provenance.ServiceLayer.Services
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IProductRepository _productRepository;

		public ProductService (IUnitOfWork unitOfWork, IProductRepository productRepository)
		{
			_unitOfWork = unitOfWork;
			_productRepository = productRepository;
		}




		public Result Add (AddProductDTO inputData)
		{
			var canCreate = Product.CanCreate(inputData.Code, inputData.Name, inputData.Description);
			if (canCreate.Count > 0)
				return Result.Error(canCreate);

			var product = Product.Create(inputData.Code, inputData.Name, inputData.Description);

			_productRepository.Add(product);

			_unitOfWork.Commit();

			var data = _productRepository.Get(product.Id).ToGetProductDTO();

			return Result.Ok(data, 201, "Object created successfuly");

		}

		public Result Get (Guid id)
		{

			var data = _productRepository.Get(id).ToGetProductDTO();

			return Result.Ok(data);

		}

		public Result Update (Guid id, UpdateProductDTO inputData)
		{
			var product = _productRepository.Get(id);

			if (product == null)
				return Result.Error("there is no product with that identifier");

			var canUpdate = Product.CanUpdate(inputData.Code, inputData.Name, inputData.Description);
			if (canUpdate.Count > 0)
				return Result.Error(canUpdate);

			product.Update(inputData.Code, inputData.Name, inputData.Description);

			_productRepository.Update(product);

			_unitOfWork.Commit();

			return Result.Ok(null, 200, "Object updated successfuly");

		}

		public Result Delete (Guid id)
		{

			var product = _productRepository.Get(id);

			if (product == null)
				return Result.Error("there is no product with that identifier");

			_productRepository.Delete(product);

			_unitOfWork.Commit();

			return Result.Ok(null, 200, "Object deleted successfuly");
		}

		public Result GetAll ()
		{
			var data = _productRepository.GetAll().ToGetProductDTOList();
			return Result.Ok(data);
		}

		public PageableResult GetAll (int page, int pageSize)
		{
			var data = _productRepository.GetAll(page, pageSize).ToGetProductDTOList();
			var count = _productRepository.GetCount();
			return PageableResult.Ok(data, count, page, pageSize);
		}

		public Result GetByTransactionId (Guid id)
		{

			var data = _productRepository.GetProductByTransactionId(id).ToGetProductDTOList();
			return Result.Ok(data);
		}

	}
}
