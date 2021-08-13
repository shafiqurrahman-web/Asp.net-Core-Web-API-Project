using Microsoft.EntityFrameworkCore;
using System;

namespace Provenance.DataLayer.Base
{
	public interface IUnitOfWork
	{
		Guid Id { get; }
		void Register (IRepository repository);
		void Commit ();
		DbSet<T> GetCollection<T> () where T : class;

	}
}
