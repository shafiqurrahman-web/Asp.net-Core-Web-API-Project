using Provenance.Common.Responses;
using Provenance.DataLayer.Base;
using Provenance.DataLayer.Entities;
using Provenance.DataLayer.Repositories;
using Provenance.ServiceLayer.Contracts;
using Provenance.ServiceLayer.DTOs.Transaction;
using Provenance.ServiceLayer.Mappings;
using System;
using System.Linq;

namespace Provenance.ServiceLayer.Services
{
	public class TransactionService : ITransactionService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ITransactionRepository _transactionRepository;
		private readonly IProviderRepository _providerRepository;
		private readonly IProductRepository _productRepository;
		private readonly IUserRepository _userRepository;
		private readonly ITransactionProductRepository _transactionProductRepository;

		public TransactionService (
			IUnitOfWork unitOfWork,
			ITransactionRepository transactionRepository,
			IProviderRepository providerRepository,
			IProductRepository productRepository,
			IUserRepository userRepository,
			ITransactionProductRepository transactionProductRepository)
		{
			_unitOfWork = unitOfWork;
			_transactionRepository = transactionRepository;
			_providerRepository = providerRepository;
			_productRepository = productRepository;
			_userRepository = userRepository;
			_transactionProductRepository = transactionProductRepository;
		}



		public Result Add (AddTransactionDTO inputData)
		{
			var canCreate = Transaction.CanCreate(inputData.ProviderId, inputData.CustomerId);
			if (canCreate.Count > 0)
				Result.Error(canCreate);

			var transaction = Transaction.Create(inputData.ProviderId, inputData.CustomerId);

			_transactionRepository.Add(transaction);

			_unitOfWork.Commit();

			var data = _transactionRepository.Get(transaction.Id).ToGetTransactionDTO();

			return Result.Ok(data, 201, "Object created successfuly");
		}

		public Result Get (Guid id)
		{
			var data = _transactionRepository.Get(id).ToGetTransactionDTO();
			return Result.Ok(data);
		}

		public Result Update (Guid id, UpdateTransactionDTO inputData)
		{

			var transaction = _transactionRepository.Get(id);

			if (transaction == null)
				return Result.Error("there is no transaction with that identifier");

			var canUpdate = transaction.CanUpdate(inputData.ProviderId, inputData.CustomerId, inputData.Complete);
			if (canUpdate.Count > 0)
				return Result.Error(canUpdate);

			transaction.Update(inputData.ProviderId, inputData.CustomerId, inputData.Complete);

			_transactionRepository.Update(transaction);

			_unitOfWork.Commit();

			return Result.Ok(null, 200, "Object updated successfuly");
		}

		public Result Delete (Guid id)
		{
			var transaction = _transactionRepository.Get(id);

			if (transaction == null)
				return Result.Error("there is no transaction with that identifier");

			_transactionRepository.Delete(transaction);

			_unitOfWork.Commit();

			return Result.Ok(null, 200, "Object deleted successfuly");
		}

		public Result GetAll ()
		{
			var data = _transactionRepository.GetAll().ToGetTransactionDTOList();
			return Result.Ok(data);
		}

		public PageableResult GetAll (int page, int pageSize)
		{
			var data = _transactionRepository.GetAll().ToGetTransactionDTOList();
			var count = _transactionRepository.GetCount();
			return PageableResult.Ok(data, count, page, pageSize);
		}



		public Result CompleteTransaction (Guid providerId)
		{
			var transaction = _transactionRepository
				.Find(x => x.ProviderId == providerId && x.IsCompleted == false);

			if (transaction == null)
				return Result.Error("there is no active transaction");

			transaction.Complete();

			_transactionRepository.Update(transaction);

			_unitOfWork.Commit();

			return Result.Ok(transaction);

		}


		public Result ScanShopCode (Guid customerId, Guid providerId)
		{
			var canCreate = Transaction.CanCreate(providerId, customerId).ToList();

			if (!_providerRepository.Exists(providerId))
				canCreate.Add(string.Format("there is no provider with this id {0}", providerId));

			if (!_userRepository.Exists(customerId))
				canCreate.Add(string.Format("there is no customer with this id {0}", customerId));

			if (canCreate.Count > 0)
				return Result.Error(canCreate);

			var transaction = Transaction.Create(providerId, customerId);

			_transactionRepository.Add(transaction);

			_unitOfWork.Commit();

			var data = _transactionRepository.GetTransaction(transaction.Id).ToGetTransactionDTO();

			return Result.Ok(data);

		}

		public Result ScanProduct (Guid providerId, Guid productId)
		{

			var canCreate = TransactionProduct.CanCreate(providerId, productId).ToList();

			if (!_providerRepository.Exists(providerId))
				canCreate.Add(string.Format("there is no active provider with this id {0}", providerId));

			if (!_productRepository.Exists(productId))
				canCreate.Add(string.Format("there is no active product with this id {0}", productId));
			
			if (canCreate.Count > 0)
				return Result.Error(canCreate);

			var transactionProduct = TransactionProduct.Create(providerId, productId);

			_transactionProductRepository.Add(transactionProduct);

			_unitOfWork.Commit();

			var data = _transactionProductRepository.GetTransactionProductData(transactionProduct.Id).ToGetTransactionProductDTO();

			return Result.Ok(data);


		}

	}
}
