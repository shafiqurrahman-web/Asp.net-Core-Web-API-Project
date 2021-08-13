using Microsoft.EntityFrameworkCore;
using Provenance.DataLayer.Base;
using Provenance.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Provenance.DataLayer.Repositories.Implementaions
{
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly DbSet<Product> _dbSet;
		private readonly DbSet<Transaction> _transactions;

		public ProductRepository (IUnitOfWork unitOfWork) : base(unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_dbSet = unitOfWork.GetCollection<Product>();
			_transactions = unitOfWork.GetCollection<Transaction>();
		}


		public IEnumerable<Product> GetProductByTransactionId (Guid id)
		{
			var query = @"SELECT P.*
						FROM [dbProvenanceSystem].[dbo].[Product] P JOIN 
							 [dbProvenanceSystem].[dbo].[TransactionProduct] TP
						ON TP.ProductId = P.Id
						WHERE TP.TransactionId = @TransactionId";
			
			var result = _dbSet.FromSqlRaw<Product>(query, id).ToList();
			return result;
		}


	}

}
