namespace api.Models.Dtos.Team
{
	public class TeamUpdateDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public TeamPlayersEditDto PlayersEdited { get; set; } = new TeamPlayersEditDto();
	}
}
