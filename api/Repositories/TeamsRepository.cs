using api.Data;
using api.Models.Entities;


namespace api.Repositories
{
	public class TeamsRepository : BaseCRUDRepository<Team>
	{
		public TeamsRepository(ApplicationDBContext db) : base(db)
		{ }


		public override Task<Team> Create(Team entity)
		{
			foreach (var p in entity.Players)
				_db.Set<Player>().Attach(p);
			
			return base.Create(entity);
		}
	}
}
