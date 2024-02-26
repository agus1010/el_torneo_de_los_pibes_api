using api.Models.Dtos.Player;


namespace api.Models.Dtos.Team
{
	public class TeamDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public ISet<PlayerDto> Players { get; set; } = new HashSet<PlayerDto>();
	}
}
