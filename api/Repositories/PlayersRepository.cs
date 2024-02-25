using api.Data;
using api.Models.Dtos.Player;
using api.Models.Entities;


namespace api.Repositories
{
	public class PlayersRepository : BaseCRUDRepository<Player>
	{
		public PlayersRepository(ApplicationDBContext db) : base(db)
		{ }

		public async Task<IEnumerable<PlayerDto>>
	}
}
