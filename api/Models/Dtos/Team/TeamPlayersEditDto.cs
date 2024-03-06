namespace api.Models.Dtos.Team
{
	public class TeamPlayersEditDto
	{
		public ISet<int> PlayersAdded { get; set; } = new HashSet<int>();
		public ISet<int> PlayersRemoved { get; set; } = new HashSet<int>();
	}
}
