using Microsoft.EntityFrameworkCore;

using api.Data;
using api.Models.Entities;


namespace api.Repositories
{
	public class TeamsRepository : BaseCRUDRepository<Team>
	{
		public TeamsRepository(ApplicationDBContext db) : base(db)
		{ }


		public async override Task<Team> Create(Team team)
		{
			setPlayersInTeamToIgnore(team);
			return await base.Create(team);
		}

		public async override Task Update(Team team)
		{
			setPlayersInTeamToIgnore(team);
			_db.Set<Team>().Entry(team).State = EntityState.Modified;
			await Persist();
		}


		private void setPlayersInTeamToIgnore(Team team)
		{
			foreach (var p in team.Players)
				_db.Set<Player>().Entry(p).State = EntityState.Unchanged;
		}
	}
}
