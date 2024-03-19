using Microsoft.EntityFrameworkCore;

using api.Models;


namespace api.Data
{
	public class DBQueryRunner<T>  where T : class, IIdentificable
	{
		protected readonly ApplicationDbContext qContext;

		public DBQueryRunner(IDbContextFactory<ApplicationDbContext> contextFactory)
			=> qContext = contextFactory.CreateDbContext();



		public virtual async Task<T?> GetAsync(int id)
			=> await GetAsync(qContext, id, track: false);

		public virtual async Task<T?> GetAsync(ApplicationDbContext context, int id, bool track = false)
			=> await GetAsync(context.Set<T>(), id, track);

		public virtual async Task<T?> GetAsync(IQueryable<T> queryable, int id, bool track = false)
			=> await getAsync(configQuery(queryable, track), id);		



		public virtual async Task<IEnumerable<T>> GetRangeAsync(IEnumerable<int> ids)
			=> await GetRangeAsync(qContext, ids, track: false);

		public virtual async Task<IEnumerable<T>> GetRangeAsync(ApplicationDbContext context, IEnumerable<int> ids, bool track = false)
			=> await GetRangeAsync(context.Set<T>(), ids, track);

		public virtual async Task<IEnumerable<T>> GetRangeAsync(IQueryable<T> queryable, IEnumerable<int> ids, bool track = false)
			=> await getRangeAsync(configQuery(queryable, track), ids);



		protected virtual IQueryable<T> configQuery(IQueryable<T> queryable, bool track)
		{
			if (!track)
				queryable = queryable.AsNoTracking();
			return queryable;
		}


		protected virtual async Task<T?> getAsync(IQueryable<T> queryable, int id)
			=> await queryable.FirstOrDefaultAsync(e => e.Id == id);

		protected virtual async Task<IEnumerable<T>> getRangeAsync(IQueryable<T> queryable, IEnumerable<int> ids)
			=> await queryable.Where(e => ids.Contains(e.Id)).ToListAsync();
	}
}