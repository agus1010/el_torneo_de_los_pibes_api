using api.Data;
using api.Models.Entities;
using api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace api.Repositories
{
    public class TeamsRepository : ITeamsRepository
	{
		protected readonly ApplicationDbContext context;

		public TeamsRepository(ApplicationDbContext context)
			=> this.context = context;


		protected IQueryable<Team> AllTeams(bool includePlayers = false, bool track = false)
		{
			IQueryable<Team> query = context.Teams.AsQueryable();
			if (includePlayers)
				query = query.Include(t => t.Players);
			if (!track)
				query = query.AsNoTracking();
			return query;
		}


		public async Task<Team> CreateAsync(Team team)
		{
			await context.Players.AddRangeAsync(team.Players);
			context.Players.AttachRange(team.Players);
			await context.Teams.AddAsync(team);
			await Persist();
			return team;
		}


		public async Task<Team?> GetAsync(int id, bool includePlayers, bool track = false)
			=> await AllTeams(includePlayers, track).FirstOrDefaultAsync(t => t.Id == id);


		public async Task UpdateAsync(Team updatedTeam)
		{
			throw new NotImplementedException();
		}


		public async Task DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}


		public async Task EditTeamPlayers(Team team, IEnumerable<Player> playersRemoved, IEnumerable<Player> playersAdded)
		{
			throw new NotImplementedException();
		}



		protected async Task Persist()
			=> await context.SaveChangesAsync();
	}
}
