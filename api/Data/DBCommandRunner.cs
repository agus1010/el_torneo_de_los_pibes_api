using Microsoft.EntityFrameworkCore;


namespace api.Data
{
	public class DBCommandRunner<T> where T : class
	{
		protected readonly IDbContextFactory<ApplicationDbContext> contextFactory;

		public DBCommandRunner(IDbContextFactory<ApplicationDbContext> contextFactory)
			=> this.contextFactory = contextFactory;



		public async Task<T> CreateAsync(T entity)
		{
			using (var context = await contextFactory.CreateDbContextAsync())
				return await CreateAsync(context, entity);
		}

		public async Task<IEnumerable<T>> CreateAsync(IEnumerable<T> entities)
		{
			using (var context = await contextFactory.CreateDbContextAsync())
				return await CreateAsync(context, entities);
		}

		public virtual async Task<T> CreateAsync(ApplicationDbContext context, T entity)
		{
			await context.Set<T>().AddAsync(entity);
			return entity;
		}

		public virtual async Task<IEnumerable<T>> CreateAsync(ApplicationDbContext context, IEnumerable<T> entities)
		{
			await context.Set<T>().AddRangeAsync(entities);
			return entities;
		}



		public async Task UpdateAsync(T entity)
		{
			using (var context = await contextFactory.CreateDbContextAsync())
				await UpdateAsync(context, entity);
		}

		public async Task UpdateAsync(IEnumerable<T> entities)
		{
			using (var context = await contextFactory.CreateDbContextAsync())
				await UpdateAsync(context, entities);
		}
		
		public virtual async Task UpdateAsync(ApplicationDbContext context, T entity)
			=> await Task.Run(() => context.Set<T>().Update(entity));
		
		public virtual async Task UpdateAsync(ApplicationDbContext context, IEnumerable<T> entities)
			=> await Task.Run(() => context.Set<T>().UpdateRange(entities));



		public async Task DeleteAsync(T entity)
		{
			using (var context = await contextFactory.CreateDbContextAsync())
				await DeleteAsync(context, entity);
		}

		public async Task DeleteAsync(IEnumerable<T> entities)
		{
			using (var context = await contextFactory.CreateDbContextAsync())
				await DeleteAsync(context, entities);
		}

		public async Task DeleteAsync(ApplicationDbContext context, T entity)
			=> await Task.Run(() => context.Set<T>().Remove(entity));

		public async Task DeleteAsync(ApplicationDbContext context, IEnumerable<T> entities)
			=> await Task.Run(() => context.Set<T>().RemoveRange(entities));
	}
}
