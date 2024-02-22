using System.ComponentModel.DataAnnotations;


namespace api.Models.Entities
{
	public class Player
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public string ImageUrl { get; set; }
	}
}