using Microsoft.EntityFrameworkCore;

using api.Data;
using api.Models.Entities;
using api.Repositories.Interfaces;


namespace api.Repositories
{
    public class TeamsRepository : ITeamsRepository
	{
		protected readonly ApplicationDbContext context;

		public TeamsRepository(ApplicationDbContext context)
			=> this.context = context;


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


		// https://learn.microsoft.com/en-us/ef/core/change-tracking/relationship-changes
		// https://learn.microsoft.com/en-us/ef/core/change-tracking/
		// https://learn.microsoft.com/en-us/ef/core/change-tracking/identity-resolution
		public async Task UpdateAsync(Team updatedTeam)
		{
			var trackedTeam = (await GetAsync(updatedTeam.Id, true, true))!;
			var originalPlayers = trackedTeam.Players.Select(p => p!);

			context.Entry(trackedTeam!).CurrentValues.SetValues(updatedTeam);

			var ignoredPlayers = playersSetIntersection(originalPlayers, updatedTeam.Players);
			foreach (var player in ignoredPlayers)
				context.Entry(player).State = EntityState.Detached;

			removePlayers(trackedTeam, playersSetDifference(originalPlayers, ignoredPlayers));
			addPlayers(trackedTeam, playersSetDifference(updatedTeam.Players, ignoredPlayers));

			await Persist();
		}


		public async Task DeleteAsync(Team team)
		{
			context.Teams.Remove(team);
			await Persist();
		}


		public async Task EditPlayers(Team team, ISet<Player> addedPlayers, ISet<Player> removedPlayers)
		{
			var trackedTeam = (await GetAsync(team.Id, true, true))!;
			addPlayers(trackedTeam, addedPlayers);
			removePlayers(trackedTeam, removedPlayers);
			await Persist();
		}


		public async Task AddPlayers(Team team, ISet<Player> addedPlayers)
		{
			var trackedTeam = (await GetAsync(team.Id, true, true))!;
			addPlayers(team, addedPlayers);
			await Persist();
		}


		public async Task RemovePlayers(Team team, ISet<Player> removedPlayers)
		{
			var trackedTeam = (await GetAsync(team.Id, true, true))!;
			removePlayers(team, removedPlayers);
			await Persist();
		}



		protected IQueryable<Team> AllTeams(bool includePlayers = false, bool track = false)
		{
			IQueryable<Team> query = context.Teams.AsQueryable();
			if (includePlayers)
				query = query.Include(t => t.Players);
			if (!track)
				query = query.AsNoTracking();
			return query;
		}


		protected async Task Persist()
			=> await context.SaveChangesAsync();


		protected void addPlayers(Team team, IEnumerable<Player> playersToAdd)
		{
			foreach (var player in playersToAdd)
				team.Players.Add(player);
		}


		protected void removePlayers(Team team, IEnumerable<Player> playersToRemove)
		{
			foreach (var player in playersSetIntersection(team.Players, playersToRemove))
				team.Players.Remove(player);
		}


		public IEnumerable<Player> playersSetIntersection(IEnumerable<Player> set1, IEnumerable<Player> set2)
			=> set1.IntersectBy(set2.Select(p => p.Id), t => t.Id);

		public IEnumerable<Player> playersSetDifference(IEnumerable<Player> set1, IEnumerable<Player> set2)
			=> set1.ExceptBy(set2.Select(p => p.Id), p => p.Id);
	}
}
