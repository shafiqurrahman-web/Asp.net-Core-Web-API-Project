using Microsoft.EntityFrameworkCore;
using Provenance.DataLayer.Base;
using Provenance.DataLayer.DataObjects;
using Provenance.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Provenance.DataLayer.Repositories.Implementaions
{
	public class ProductHistoryRepository : Repository<ProductHistory>, IProductHistoryRepository
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly DbSet<ProductHistory> _dbSet;

		public ProductHistoryRepository (IUnitOfWork unitOfWork) : base(unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_dbSet = unitOfWork.GetCollection<ProductHistory>();
		}

		public ProductHistoryDataObject GetById (Guid id)
		{
			return _dbSet.Where(e => e.Id.Equals(id))
				.Include(e => e.Product)
				.Select(e => new ProductHistoryDataObject
				{
					Id = e.Id,
					Title = e.Title,
					Content = e.Content,
					Picture = e.Picture,
					ProductId = e.ProductId,
					ProductName = e.Product.Name,
					Date = e.Date,
					LocationTitle = e.LocationTitle,
					LocationAddress = e.LocationAddress
				}).FirstOrDefault();
		}

		public IEnumerable<ProductHistoryDataObject> GetByCondition (Expression<Func<ProductHistory, bool>> predicate)
		{
			return _dbSet.Where(predicate)
				.Include(e => e.Product)
				.Select(e => new ProductHistoryDataObject
				{
					Id = e.Id,
					Title = e.Title,
					Content = e.Content,
					Picture = e.Picture,
					ProductId = e.ProductId,
					ProductName = e.Product.Name,
					Date = e.Date,
					LocationTitle = e.LocationTitle,
					LocationAddress = e.LocationAddress
				}).ToList();
		}

		public IEnumerable<ProductHistoryDataObject> GetProductHistoryByProductId (Guid id)
		{
			return _dbSet.Where(e => e.Id.Equals(id))
				.Include(e => e.Product)
				.Select(e => new ProductHistoryDataObject
				{
					Id = e.Id,
					Title = e.Title,
					Content = e.Content,
					Picture = e.Picture,
					ProductId = e.ProductId,
					ProductName = e.Product.Name,
					Date = e.Date,
					LocationTitle = e.LocationTitle,
					LocationAddress = e.LocationAddress
				}).ToList();
		}


	}
}
