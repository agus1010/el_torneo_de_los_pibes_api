using Microsoft.EntityFrameworkCore;

using api.Models.Entities;


namespace api.Data.Queries
{
	public class TeamQueriesRunner : DBQueryRunner<Team>
	{
		public TeamQueriesRunner(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory)
		{ }



		public virtual async Task<Team?> GetAsync(int id, bool fetchPlayers = false)
			=> await getAsync(
					queryable: configQuery(
						queryable: qContext.Teams,
						track: false,
						fetchPlayers: fetchPlayers,
						trackPlayers: false
					),
					id: id
				);

		public virtual async Task<Team?> GetAsync(ApplicationDbContext context, int id, bool track = false, bool fetchPlayers = false, bool trackPlayers = false)
			=> await getAsync (
					queryable: configQuery (
						queryable: context.Teams, 
						track: track, 
						fetchPlayers: fetchPlayers, 
						trackPlayers: trackPlayers
					),
					id: id
				);




		public virtual async Task<IEnumerable<Team>> GetRangeAsync(IEnumerable<int> ids, bool fetchPlayers = false)
			=> await getRangeAsync (
					queryable: configQuery (
						queryable: qContext.Teams,
						track: false,
						fetchPlayers: fetchPlayers,
						trackPlayers: false
					),
					ids: ids
				);

		public virtual async Task<IEnumerable<Team>> GetRangeAsync(ApplicationDbContext context, IEnumerable<int> ids, bool track = false, bool fetchPlayers = false, bool trackPlayers = false)
			=> await getRangeAsync (
				queryable: configQuery (
					queryable: context.Teams, 
					track: track, 
					fetchPlayers: fetchPlayers, 
					trackPlayers: trackPlayers
				),
				ids: ids
			);




		protected IQueryable<Team> configQuery(IQueryable<Team> queryable, bool track, bool fetchPlayers = false, bool trackPlayers = false)
		{
			if (fetchPlayers)
			{
				queryable = queryable.Include(t => t.Players);
				if (!trackPlayers)
					queryable = queryable.AsNoTrackingWithIdentityResolution();
			}
			return base.configQuery(queryable, track);
		}
	}
}
