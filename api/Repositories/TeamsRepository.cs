using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

using api.Models.Entities;
using api.Data;


namespace api.Repositories
{
	public class TeamsRepository : BaseCRUDRepository<Team>
	{
		public TeamsRepository(ApplicationDBContext db) : base(db)
		{ }


		protected override IQueryable<Team> configQuery(IQueryable<Team> query, Expression<Func<Team, bool>>? filter, bool tracked)
		{
			query = query.Include("Players");
			return base.configQuery(query, filter, tracked);
		}
	}
}
