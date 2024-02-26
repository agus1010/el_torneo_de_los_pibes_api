﻿using System.ComponentModel.DataAnnotations;


namespace api.Models.Entities
{
	public class Team
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;

		// navigation
		public ICollection<Player>? Players { get; set; }
	}
}