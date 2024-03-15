using Microsoft.EntityFrameworkCore;

using api.Models;


namespace api.Data
{
	public class DBQueryRunner<T>  where T : class, IIdentificable
	{
		protected readonly ApplicationDbContext qContext;

		public DBQueryRunner(IDbContextFactory<ApplicationDbContext> contextFactory)
			=> qContext = contextFactory.CreateDbContext();


		protected virtual IQueryable<T> AllEntities
			=> qContext.Set<T>().AsNoTracking();



		public virtual async Task<T?> GetAsync(int id)
			=> await getAsync(AllEntities, id);

		public virtual async Task<IEnumerable<T>> GetAsync(IEnumerable<int> ids)
			=> await getAsync(AllEntities, ids);



		protected virtual async Task<T?> getAsync(IQueryable<T> queriable, int id)
			=> await queriable.FirstOrDefaultAsync(e => e.Id == id);

		protected virtual async Task<IEnumerable<T>> getAsync(IQueryable<T> queriable, IEnumerable<int> ids)
			=> await queriable.Where(e => ids.Contains(e.Id)).ToListAsync();
	}
}
