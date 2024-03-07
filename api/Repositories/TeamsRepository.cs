using api.Data;
using api.Models.Entities;
using api.Repositories.Interfaces;
using api.Services.Errors;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace api.Repositories
{
    public class TeamsRepository
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


		// https://learn.microsoft.com/en-us/ef/core/change-tracking/relationship-changes
		// https://learn.microsoft.com/en-us/ef/core/change-tracking/
		// https://learn.microsoft.com/en-us/ef/core/change-tracking/identity-resolution
		public async Task UpdateAsync(Team updatedTeam)
		{
			var trackedTeam = await AllTeams(includePlayers: true, track: true).FirstAsync(t => t.Id == updatedTeam.Id);
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


		public async Task AddPlayers(Team team, ISet<Player> addedPlayers)
		{
			var trackedTeam = (await context.Teams.FindAsync(team.Id))!;
			foreach (var player in addedPlayers)
				trackedTeam.Players.Add(player);
			await Persist();
        }


		public async Task RemovePlayers(Team team, ISet<Player> removedPlayers)
		{
			var trackedTeam = (await context.Teams.FindAsync(team.Id))!;
			foreach (var player in removedPlayers)
				trackedTeam.Players.Remove(player);
			await Persist();
		}


		protected async Task Persist()
			=> await context.SaveChangesAsync();
	}
}
