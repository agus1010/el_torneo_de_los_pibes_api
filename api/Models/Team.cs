using System.ComponentModel.DataAnnotations;


namespace api.Models
{
	public class Team
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		public ICollection<Player> Players { get; set; }
	}
}
