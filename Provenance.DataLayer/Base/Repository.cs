using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Provenance.DataLayer.Base
{
	public abstract class Repository<T> : IRepository<T> where T : EntityBase
	{

		private readonly DbSet<T> _dbSet;

		protected Repository (IUnitOfWork unitOfWork)
		{
			_dbSet = unitOfWork.GetCollection<T>();
		}

		public void Add (T entity)
		{
			_dbSet.Add(entity);
		}

		public T Get<Tkey> (Tkey id)
		{
			return _dbSet.Find(id);
		}
		public async Task<T> GetAsync<Tkey> (Tkey id)
		{
			return await _dbSet.FindAsync(id);
		}
		public T Get (params object[] keyValues)
		{
			return _dbSet.Find(keyValues);
		}
		public T Get (Guid id)
		{
			return _dbSet.Find(id);
		}

		public void Update (T entity)
		{
			_dbSet.Update(entity);
		}

		public void Delete (T entity)
		{
			_dbSet.Remove(entity);
		}

		public bool Exists (Expression<Func<T, bool>> predicate)
		{
			return _dbSet.Any(predicate);
		}
		public bool Exists (Guid id)
		{
			return _dbSet.Any(e => e.Id == id);
		}
		
		public IQueryable<T> FindBy (Expression<Func<T, bool>> predicate)
		{
			return _dbSet.Where(predicate);
		}
		public IEnumerable<T> FindBy (Expression<Func<T, bool>> predicate, string include)
		{
			return _dbSet.Where(predicate).Include(include).ToList();
		}
		public IEnumerable<T> FindBy (Expression<Func<T, bool>> predicate, string include, string include2)
		{
			return _dbSet.Where(predicate).Include(include).Include(include2).ToList();
		}
		public T Find (Expression<Func<T, bool>> predicate)
		{
			return _dbSet.Where(predicate).FirstOrDefault();
		}


		public IQueryable<T> FromSql (string query, params object[] parameters)
		{
			return _dbSet.FromSqlRaw(query, parameters);
		}
		public IQueryable<T> FromSql (string sqlQuery)
		{
			return _dbSet.FromSqlRaw(sqlQuery);
		}

		public async Task<IList<T>> GetAllAsync (int page, int pageCount, Expression<Func<T, bool>> predicate, string include)
		{
			var pageSize = (page - 1) * pageCount;

			return await _dbSet.Where(predicate).Skip(pageSize).Take(pageCount).Include(include).ToListAsync();
		}
		public IList<T> GetAll ()
		{
			return _dbSet.AsNoTracking().ToList();
		}
		public IList<T> GetAll (int page, int pageCount)
		{			
			var pageSize = (page - 1) * pageCount;

			return _dbSet.AsNoTracking().Skip(pageSize).Take(pageCount).ToList();
		}
		public Task<List<T>> GetAllAsync ()
		{
			return _dbSet.AsNoTracking().ToListAsync();
		}
		public async Task<IList<T>> GetAllAsync (int page, int pageCount)
		{
			var pageSize = (page - 1) * pageCount;

			return await _dbSet.Skip(pageSize).Take(pageCount).ToListAsync();
		}

		public async Task<long> GetCountAsync ()
		{
			return await _dbSet.LongCountAsync();
		}
		public long GetCount ()
		{
			return _dbSet.LongCount();
		}




		//private readonly IDbConnection conn;

		//public Repository (string connectionString)
		//{
		//	conn = new SqlConnection(connectionString);
		//}


		//public void Add (T inputData)
		//{
		//	conn.Insert<T>(inputData);
		//}

		//public T Get (Guid id)
		//{
		//	var result = conn.Get<T>(id);
		//	return result;
		//}

		//public void Update (T inputData)
		//{
		//	conn.Update<T>(inputData);
		//}

		//public void Delete (T inputData)
		//{
		//	conn.Delete<T>(inputData);
		//}

		//public IEnumerable<T> GetAll ()
		//{
		//	var result = conn.GetAll<T>().ToList();
		//	return result;
		//}

		//public bool Exists (Guid id)
		//{
		//	var result = conn.QueryFirstOrDefault<bool> ("SELECT COUNT(*) FROM " + typeof(T).Name + " WHERE Id = @id", id);
		//	return result;
		//}

		//public IEnumerable<T> FindBy (Expression<Func<T, bool>> predicate)
		//{
		//	var builder = new WhereBuilder();
		//	var whereClause = builder.ToRawSql(predicate);
		//	var tableName = typeof(T).Name;
		//	var result = conn.Query<T>($"SELECT * FROM {tableName} WHERE {whereClause}");
		//	return result;
		//}





	}
}
