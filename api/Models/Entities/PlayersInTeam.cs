using System.ComponentModel.DataAnnotations;


namespace api.Models.Entities
{
	public class PlayersInTeam
	{
		[Key] public int Id { get; set; }
		[Required] public Player Player { get; set; }
	}
}