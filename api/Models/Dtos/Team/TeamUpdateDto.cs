namespace api.Models.Dtos.Team
{
	public class TeamUpdateDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ISet<int> PlayerIds { get; set; } = new HashSet<int>();
	}
}
