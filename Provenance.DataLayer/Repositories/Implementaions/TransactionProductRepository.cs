using Microsoft.EntityFrameworkCore;
using Provenance.DataLayer.Base;
using Provenance.DataLayer.DataObjects;
using Provenance.DataLayer.Entities;
using System;
using System.Linq;

namespace Provenance.DataLayer.Repositories.Implementaions
{
	public class TransactionProductRepository : Repository<TransactionProduct>, ITransactionProductRepository
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly DbSet<TransactionProduct> _dbSet;

		public TransactionProductRepository (IUnitOfWork unitOfWork) : base(unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_dbSet = unitOfWork.GetCollection<TransactionProduct>();
		}

		public TransactionProductDataObject GetTransactionProductData (Guid id)
		{
			return _dbSet.Where(e => e.Id.Equals(id))
				.Include(e => e.Product)
				.Include(e => e.Transaction)
					.ThenInclude(e => e.Provider)
				.Select(e => new TransactionProductDataObject
				{
					Id = e.Id,
					CreateDate = e.CreateDate,
					TransactionId = e.TransactionId,
					TransactionCreateDate = e.Transaction.CreateDate,
					TransactionCustomerId = e.Transaction.CustomerId,
					TransactionProviderId = e.Transaction.ProviderId,
					TransactionProviderName = e.Transaction.Provider.Name,
					ProductId = e.ProductId,
					ProductCode = e.Product.Code,
					ProductName = e.Product.Name
				})
				.FirstOrDefault();
		}
	}
}
