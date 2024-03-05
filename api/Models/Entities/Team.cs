using System.ComponentModel.DataAnnotations;


namespace api.Models.Entities
{
	public class Team
	{
		[Key]
		public int Id { get; set; }
		[Required] public string Name { get; set; } = "Team";

		// navigation
		public virtual ISet<Player> Players { get; set; } = new HashSet<Player>();
	}
}