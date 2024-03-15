using System.ComponentModel.DataAnnotations;


namespace api.Models.Entities
{
	public class Bet: IIdentificable
	{
		[Key] public int Id { get; set; }
		[Required] public Player Creator { get; set; }
		[Required] public Match Match { get; set; }
		[Required] public Team WiningTeam { get; set; }
		public DateTime CreationDate { get; set; } = DateTime.Now;
	}
}