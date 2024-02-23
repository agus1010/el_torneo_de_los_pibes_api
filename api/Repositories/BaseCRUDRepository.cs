using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

using api.Data;
using api.Repositories.Interfaces;


namespace api.Repositories
{
	public class BaseCRUDRepository<T> : IBaseCRUDRepository<T> where T : class
	{
		protected readonly ApplicationDBContext _db;
		internal readonly DbSet<T> dbSet;

		public BaseCRUDRepository(ApplicationDBContext db)
		{
			_db = db;
			dbSet = _db.Set<T>();
		}


		public async Task<T> Create(T entity)
		{
			await dbSet.AddAsync(entity);
			await Persist();
			return entity;
		}

		public async Task<T?> ReadSingle(Expression<Func<T, bool>>? filter = null, bool tracked = true)
		{
			var query = configQuery(dbSet, filter, tracked);
			return await query.FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<T>> ReadMany(Expression<Func<T, bool>>? filter = null, bool tracked = true)
		{
			var query = configQuery(dbSet, filter, tracked);
			return await query.ToListAsync();
		}

		public async Task Update(T entity)
		{
			_db.Entry(entity).State = EntityState.Modified;
			try
			{
				await Persist();
			}
			catch (DbUpdateConcurrencyException)
			{
				throw;
			}
		}

		public async Task Delete(T entity)
		{
			dbSet.Remove(entity);
			await Persist();
		}

		public async Task Persist()
		{
			await _db.SaveChangesAsync();
		}

		private IQueryable<T> configQuery(IQueryable<T> query, Expression<Func<T, bool>>? filter, bool tracked)
		{
			if (!tracked)
				query = query.AsNoTracking();
			if (filter != null)
				query = query.Where(filter);
			return query;
		}
	}
}