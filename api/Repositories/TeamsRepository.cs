using Microsoft.EntityFrameworkCore;

using api.Data;
using api.Models.Entities;
using NuGet.Packaging;


namespace api.Repositories
{
    public class TeamsRepository
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

			var ignoredPlayers = originalPlayers.IntersectBy(updatedTeam.Players.Select(p => p.Id), p => p.Id);
			foreach (var player in ignoredPlayers)
				context.Entry(player).State = EntityState.Detached;

			var removedPlayers = originalPlayers.ExceptBy(ignoredPlayers.Select(p => p.Id), p => p.Id);
			foreach (var player in removedPlayers)
				trackedTeam.Players.Remove(player);

			var addedPlayers = updatedTeam.Players.ExceptBy(ignoredPlayers.Select(p => p.Id), p => p.Id).ToList();
			foreach (var player in addedPlayers)
				trackedTeam.Players.Add(player);

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

			foreach (var player in addedPlayers)
				trackedTeam.Players.Add(player);

			var tracekdPlayersRemoved = trackedTeam.Players.Where(p => removedPlayers.Select(p => p.Id).Contains(p.Id));
			foreach (var referencedPlayer in tracekdPlayersRemoved)
				trackedTeam.Players.Remove(referencedPlayer);
			
			await Persist();
		}


		public async Task AddPlayers(Team team, ISet<Player> addedPlayers)
		{
			var trackedTeam = (await GetAsync(team.Id, true, true))!;
			foreach (var player in addedPlayers)
				trackedTeam.Players.Add(player);
			await Persist();
        }


		public async Task RemovePlayers(Team team, ISet<Player> removedPlayers)
		{
			var trackedTeam = (await GetAsync(team.Id, true, true))!;
			foreach (var player in removedPlayers)
				trackedTeam.Players.Remove(player);
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
			=> team.Players.AddRange(playersToAdd);


		protected void removePlayers(Team team, IEnumerable<Player> playersToRemove)
		{
			foreach (var player in team.Players.IntersectBy(playersToRemove.Select(p => p.Id), t => t.Id))
				team.Players.Remove(player);
		}
	}
}
