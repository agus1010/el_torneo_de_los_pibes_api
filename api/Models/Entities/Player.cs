using System.ComponentModel.DataAnnotations;


namespace api.Models.Entities
{
	public class Player
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } = string.Empty;
		public string? ImageUrl { get; set; }

		//public ICollection<Team>? Teams { get; set; }
	}
}