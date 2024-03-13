using Microsoft.EntityFrameworkCore;

using api.Data;
using api.Models.Entities;
using api.Repositories.Interfaces;


namespace api.Repositories
{
	public class PlayersRepository : IPlayersRepository
	{
		protected IDbContextFactory<ApplicationDbContext> contextFactory;
		protected ApplicationDbContext qContext;

		protected IQueryable<Player> allPlayers => qContext.Players.AsNoTracking();


		public PlayersRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
		{
			this.contextFactory = contextFactory;
			qContext = contextFactory.CreateDbContext();
		}


		public async Task<IEnumerable<Player>> CreateAsync(IEnumerable<Player> players)
		{
			using (var context = contextFactory.CreateDbContext())
				await context.Players.AddRangeAsync(players);
			return players;
		}

		public async Task<Player> CreateAsync(Player player)
		{
			using (var context = await contextFactory.CreateDbContextAsync())
				await context.Players.AddAsync(player);
			return player;
		}

		public async Task DeleteAsync(Player player)
		{
			using (var context = await contextFactory.CreateDbContextAsync())
				context.Players.Remove(player);
		}

		public async Task<IEnumerable<Player>> GetAsync(IEnumerable<int> ids)
			=> await allPlayers.Where(p => ids.Contains(p.Id)).ToListAsync();

		public async Task<Player?> GetAsync(int id)
			=> await allPlayers.FirstOrDefaultAsync(p => p.Id == id);


		public async Task<IEnumerable<Player>> UpdateAsync(IEnumerable<Player> players)
		{
			using (var context = await contextFactory.CreateDbContextAsync())
				context.Players.UpdateRange(players);
			return players;
		}

		public async Task<Player> UpdateAsync(Player player)
		{
			using (var context = await contextFactory.CreateDbContextAsync())
				context.Players.Update(player);
			return player;
		}
	}
}