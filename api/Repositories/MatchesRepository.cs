using Microsoft.EntityFrameworkCore;

using api.Data;
using api.Models.Entities;


namespace api.Repositories
{
	public class MatchesRepository
	{
		protected readonly ApplicationDbContext context;
		public MatchesRepository(ApplicationDbContext context)
		{
			this.context = context;
		}


		public async Task<Match> CreateAsync(Match match, bool track = false)
		{
			var newMatchEntity = await context.Matches.AddAsync(match);
			await Persist();
			if (!track)
				context.Entry(newMatchEntity).State = EntityState.Detached;
			return match;
		}


		public async Task<Match?> GetAsync(int id)
		{
			throw new NotImplementedException();
		}


		public async Task<IEnumerable<Match>> GetTeamsMatchesAsync(int teamId)
		{
			throw new NotImplementedException();
		}


		public async Task UpdateAsync(Match match)
		{
			throw new NotImplementedException();
		}


		public async Task DeleteAsync(Match match)
		{
			throw new NotImplementedException();
		}



		protected async Task Persist()
			=> await context.SaveChangesAsync();
	}
}