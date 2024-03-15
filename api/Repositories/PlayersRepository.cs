using api.Data;
using api.Models.Entities;
using api.Repositories.Interfaces;


namespace api.Repositories
{
    public class PlayersRepository : IPlayersRepository
	{
		protected DBCommandRunner<Player> dbCommands;
		protected DBQueryRunner<Player> qPlayers;


		public PlayersRepository(DBCommandRunner<Player> dbCommands, DBQueryRunner<Player> playerQueries)
		{
			this.dbCommands = dbCommands;
			qPlayers = playerQueries;
		}


		public async Task<IEnumerable<Player>> GetAsync(IEnumerable<int> ids)
			=> await qPlayers.GetAsync(ids);

		public async Task<Player?> GetAsync(int id)
			=> await qPlayers.GetAsync(id);


		public async Task<IEnumerable<Player>> CreateAsync(IEnumerable<Player> players)
			=> await dbCommands.CreateAsync(players);

		public async Task<Player> CreateAsync(Player player)
			=> await dbCommands.CreateAsync(player);

		public async Task DeleteAsync(Player player)
			=> await dbCommands.DeleteAsync(player);

		public async Task<IEnumerable<Player>> UpdateAsync(IEnumerable<Player> players)
		{
			await dbCommands.UpdateAsync(players);
			return players;
		}

		public async Task<Player> UpdateAsync(Player player)
		{
			await dbCommands.UpdateAsync(player);
			return player;
		}
	}
}