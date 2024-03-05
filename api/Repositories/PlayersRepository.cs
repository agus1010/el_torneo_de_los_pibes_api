using Microsoft.EntityFrameworkCore;

using api.Data;
using api.Models.Entities;
using api.Repositories.Interfaces;


namespace api.Repositories
{
    public class PlayersRepository : IPlayersRepository
	{
		protected readonly ApplicationDbContext _context;


		public PlayersRepository(ApplicationDbContext context)
			=> _context = context;



		public virtual async Task<Player?> GetAsync(int id, bool track = false)
			=> await AllPlayers(track).FirstOrDefaultAsync(p => p.Id == id);

		public virtual async Task<IEnumerable<Player?>> GetAsync(IEnumerable<int> ids, bool track = false)
			=> await AllPlayers(track).Where(p => ids.Contains(p.Id)).ToListAsync();


		public virtual async Task<Player> CreateAsync(Player player, bool keepAsTracked = false)
			=> (await CreateAsync(new[] { player }, keepAsTracked)).First();

		public virtual async Task<IEnumerable<Player>> CreateAsync(IEnumerable<Player> players, bool keepAsTracked = false)
		{
			await _context.Players.AddRangeAsync(players);
			await Persist();
			if (!keepAsTracked)
				markWithState(players, EntityState.Detached);
			return players;
		}


		public virtual async Task<Player> UpdateAsync(Player player, bool keepAsTracked = false)
			=> (await UpdateAsync(new[] { player }, keepAsTracked)).First();

		public virtual async Task<IEnumerable<Player>> UpdateAsync(IEnumerable<Player> players, bool keepAsTracked = false)
		{
			markWithState(players, EntityState.Modified);
			await Persist();
			if (!keepAsTracked)
				markWithState(players, EntityState.Detached);
			return players;
		}


		public virtual async Task DeleteAsync(int id)
		{
			Player? player = await GetAsync(id);
			if (player == null)
				return;
			await DeleteAsync(player);
		}

		public virtual async Task DeleteAsync(Player player)
		{
			_context.Players.Remove(player);
			await Persist();
		}



		protected async Task Persist()
			=> await _context.SaveChangesAsync();


		protected void markWithState(IEnumerable<Player> players, EntityState newEntityState)
		{
			foreach (var player in players)
				markWithState(player, newEntityState);
		}

		protected void markWithState(Player player, EntityState newEntityState)
			=> _context.Players.Entry(player).State = newEntityState;


		protected IQueryable<Player> AllPlayers(bool track = false)
		{
			IQueryable<Player> players = _context.Players.AsQueryable();
			if (!track)
				players = players.AsNoTracking();
			return players;
		}
	}
}
