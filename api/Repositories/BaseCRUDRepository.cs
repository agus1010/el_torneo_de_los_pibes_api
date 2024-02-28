﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

using api.Data;
using api.Repositories.Interfaces;


namespace api.Repositories
{
	public class BaseCRUDRepository<T> : IBaseCRUDRepository<T> where T : class
	{
		protected readonly ApplicationDBContext _context;
		protected DbSet<T> dbSet;

		public BaseCRUDRepository(ApplicationDBContext db)
		{
			_context = db;
			dbSet = _context.Set<T>();
		}


		public virtual async Task<T> Create(T entity)
		{
			await dbSet.AddAsync(entity);
			await Persist();
			return entity;
		}

		public virtual async Task<T?> ReadSingle(Expression<Func<T, bool>>? filter = null, bool tracked = true, string? includeField = null)
		{
			var query = configQuery(dbSet, filter, tracked, includeField);
			return await query.FirstOrDefaultAsync();
		}

		public virtual async Task<IEnumerable<T>> ReadMany(Expression<Func<T, bool>>? filter = null, bool tracked = true, string? includeField = null)
		{
			var query = configQuery(dbSet, filter, tracked, includeField);
			return await query.ToListAsync();
		}

		public virtual async Task Update(T entity)
		{
			dbSet.Update(entity);
			await Persist();
		}

		public virtual async Task Delete(T entity)
		{
			dbSet.Remove(entity);
			await Persist();
		}

		public virtual async Task Persist()
		{
			await _context.SaveChangesAsync();
		}


		protected virtual IQueryable<T> configQuery(IQueryable<T> query, Expression<Func<T, bool>>? filter, bool tracked, string? includeField = null)
		{
			if (!tracked)
				query = query.AsNoTracking();
			if (filter != null)
				query = query.Where(filter);
			if (includeField != null)
				query = query.Include(includeField);
			return query;
		}
	}
}