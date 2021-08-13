using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Provenance.DataLayer.Base
{
	public class UnitOfWork : IUnitOfWork
	{

		IDbContext _context;
		private Dictionary<string, IRepository> _repositories;
		//public event EventHandler OnCommit = (e, a) => { };
		//public event EventHandler OnError = (e, a) => { };
		private Guid _id;

		public UnitOfWork (IDbContext context)
		{
			_context = context;
			_repositories = new Dictionary<string, IRepository>();
			_id = Guid.NewGuid();
		}


		public Guid Id => _id;
		public void Register (IRepository repository) =>
			_repositories.TryAdd(repository.GetType().ToString(), repository);


		public void Commit ()
		{
			try
			{
				//add strategies
				_context.SaveChanges();
				//OnCommit(this, null);
			}
			catch (Exception)
			{
				//add failure strategies
				//OnError(this, null);
				throw;
			}
		}

		public DbSet<T> GetCollection<T> () where T : class
		{
			return _context.GetDbSet<T>();
		}
	}
}
