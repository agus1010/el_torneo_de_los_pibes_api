using Microsoft.EntityFrameworkCore;

using api.Data;
using api.Models.Entities;
using api.Repositories.Interfaces;
using NuGet.Packaging;

namespace api.Repositories
{
	// https://learn.microsoft.com/en-us/ef/core/change-tracking/relationship-changes
	// https://learn.microsoft.com/en-us/ef/core/change-tracking/
	// https://learn.microsoft.com/en-us/ef/core/change-tracking/identity-resolution
	// https://stackoverflow.com/questions/48962213/updating-many-to-many-in-entity-framework-core
	public class TeamsRepository : ITeamsRepository
	{
		protected IDbContextFactory<ApplicationDbContext> contextFactory;
		protected readonly ApplicationDbContext qContext;

		protected IQueryable<Team> allTeams => qContext.Teams.AsNoTracking();
		protected IQueryable<Team> allTeamsNav => allTeams.Include(t => t.Players);


		
		public TeamsRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
		{
			this.contextFactory = contextFactory;
			qContext = contextFactory.CreateDbContext();
		}



		// Queries
		public async Task<Team?> GetAsync(int id)
			=> await GetAsync(id, true);

		public async Task<Team?> GetAsync(int id, bool includePlayers = true)
			=> await (includePlayers ? allTeamsNav : allTeams).FirstOrDefaultAsync(t => t.Id == id);



		// Commands
		public async Task<Team> CreateAsync(Team team)
		{
			using (var context = await contextFactory.CreateDbContextAsync())
			{
				await context.Teams.AddAsync(team);
				// agrega los nuevos players que no existan
				await context.Players.AddRangeAsync(team.Players);
				context.Players.AttachRange(team.Players);
			}
			return team;
		}

		public async Task DeleteAsync(Team team)
		{
			using (var context = await contextFactory.CreateDbContextAsync())
				context.Teams.Remove(team);
		}

		public async Task UpdateAsync(Team updatedTeam)
		{
			var originalTeam = (await allTeamsNav.FirstAsync(t => t.Id == updatedTeam.Id));
			await editTeam(
				team: updatedTeam,
				updateScalars: true,
				playersToAdd: playersDifference(originalTeam, updatedTeam),
				playersToRemove: playersDifference(updatedTeam, originalTeam)
			);
		}



		// Players Edit Commands
		public async Task EditPlayers(Team team, ISet<Player> addedPlayers, ISet<Player> removedPlayers)
			=> await editTeam(team, playersToAdd: addedPlayers, playersToRemove: removedPlayers);

		public async Task AddPlayers(Team team, ISet<Player> addedPlayers)
			=> await editTeam(team, playersToAdd: addedPlayers);

		public async Task RemovePlayers(Team team, ISet<Player> removedPlayers)
			=> await editTeam(team, playersToRemove: removedPlayers);



		protected async Task<Team?> findTeam(ApplicationDbContext context, int id)
			=> await context.Teams.Include(t => t.Players).FirstOrDefaultAsync(t => t.Id == id);


		protected async Task editTeam(Team team, bool updateScalars = false, IEnumerable<Player>? playersToAdd = null, IEnumerable<Player>? playersToRemove = null)
		{
			using (var context = await contextFactory.CreateDbContextAsync())
			{
				var trackedTeam = (await findTeam(context, team.Id))!;
				
				if (updateScalars)
					context.Entry(trackedTeam).CurrentValues.SetValues(team);

				if (playersToRemove != null)
					removePlayersFromTeam(trackedTeam, playersToRemove);

				if (playersToAdd != null)
					addPlayersToTeam(trackedTeam, playersToAdd);
			}
		}


		protected void addPlayersToTeam(Team trackedTeam, IEnumerable<Player> playersToAdd)
			=> trackedTeam.Players.AddRange(playersToAdd);

		protected void removePlayersFromTeam(Team trackedTeam, IEnumerable<Player> playersToRemove)
		{
			foreach (var player in playersToRemove)
				trackedTeam.Players.Remove(player);
		}

		
		private IEnumerable<Player> playersDifference(Team from, Team with)
			=> playersDifference(from.Players, with.Players);

		private IEnumerable<Player> playersDifference(IEnumerable<Player> from, IEnumerable<Player> with)
			=> from.ExceptBy(with.Select(p => p.Id), p => p.Id);
	}
}
		/*public async Task<Team> CreateAsync(Team team, bool track = false)
		{
			using (var context = await contextFactory.CreateDbContextAsync())
			{
				foreach (var player in team.Players)
					context.Entry(player).State = EntityState.Detached;
				// await context.Players.AddRangeAsync(team.Players);
				// context.Players.AttachRange(team.Players);
				var newTeamEntry = await context.Teams.AddAsync(team);
				if (!track)
					newTeamEntry.State = EntityState.Detached;
			}
			return team;
		}


		public async Task<Team?> GetAsync(int id, bool includePlayers, bool track = false)
			=> await allTeamsNav.FirstOrDefaultAsync(t => t.Id == id);


		
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
*/