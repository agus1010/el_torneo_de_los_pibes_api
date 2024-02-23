using api.Data;
using api.Models.Entities;


namespace api.Repositories
{
	public class PlayersRepository : BaseCRUDRepository<Player>
	{
		public PlayersRepository(ApplicationDBContext db) : base(db)
		{
		}
	}
}
