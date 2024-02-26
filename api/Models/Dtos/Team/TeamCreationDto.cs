namespace api.Models.Dtos.Team
{
	public class TeamCreationDto
	{
		public string Name { get; set; } = string.Empty;
		public ISet<int> PlayerIds { get; set; } = new HashSet<int>();
	}
}
