using System.ComponentModel.DataAnnotations;


namespace api.Models
{
	public class Player
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public string ImageUrl { get; set; }


		public DateTime CreationDate { get; set; }
		public DateTime ModificationDate { get; set;}
	}
}