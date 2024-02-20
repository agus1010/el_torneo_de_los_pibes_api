using Microsoft.EntityFrameworkCore;

using api.Models;


namespace api.Data
{
	public class ApplicationDBContext : DbContext
	{
		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
		
		public DbSet<Player> Players { get; set; }
	}
}
