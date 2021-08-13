using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Provenance.DataLayer.Base
{
	public interface IRepository { }

	public interface IRepository<T> : IRepository where T : EntityBase
	{
		void Add (T inputData);

		T Get<Tkey> (Tkey id);
		Task<T> GetAsync<Tkey> (Tkey id);
		T Get (params object[] keyValues);
		T Get (Guid id);

		void Update (T inputData);

		void Delete (T inputData);

		bool Exists (Expression<Func<T, bool>> predicate);
		bool Exists (Guid id);

		IQueryable<T> FindBy (Expression<Func<T, bool>> predicate);
		IEnumerable<T> FindBy (Expression<Func<T, bool>> predicate, string include);
		IEnumerable<T> FindBy (Expression<Func<T, bool>> predicate, string include, string include2);

		T Find (Expression<Func<T, bool>> predicate);

		IQueryable<T> FromSql (string query, params object[] parameters);
		IQueryable<T> FromSql (string sqlQuery);

		Task<IList<T>> GetAllAsync (int page, int pageCount, Expression<Func<T, bool>> predicate, string include);
		IList<T> GetAll ();
		IList<T> GetAll (int page, int pageCount);
		Task<List<T>> GetAllAsync ();
		Task<IList<T>> GetAllAsync (int page, int pageCount);

		Task<long> GetCountAsync ();
		long GetCount ();
	}
}
