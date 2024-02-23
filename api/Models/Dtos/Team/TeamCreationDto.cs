namespace api.Models.Dtos.Team
{
	public class TeamCreationDto
	{
		public string Name { get; set; } = string.Empty;
		public IEnumerable<int> PlayerIds { get; set; } = new List<int>();
	}
}
