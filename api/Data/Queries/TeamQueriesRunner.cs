using Microsoft.EntityFrameworkCore;

using api.Models.Entities;


namespace api.Data.Queries
{
	public class TeamQueriesRunner : DBQueryRunner<Team>
	{
		public TeamQueriesRunner(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory)
		{ }

		protected virtual IQueryable<Team> AllTeamsWithNavs => AllEntities.Include(t => t.Players).AsNoTrackingWithIdentityResolution();


		public virtual async Task<Team?> GetAsyncWithNavs(int id)
			=> await getAsync(AllTeamsWithNavs, id);

		public virtual async Task<IEnumerable<Team>> GetAsyncWithNavs(IEnumerable<int> ids)
			=> await getAsync(AllTeamsWithNavs, ids);
	}
}
